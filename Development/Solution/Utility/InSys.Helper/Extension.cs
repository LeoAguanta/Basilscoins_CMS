﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using z.Data;
namespace InSys.Helper
{
    public static class Extension
    {
        public static TableData<T> QueryTable<T>(this DbContext source, string Query, TableOptions to, params object[] parameters) where T : class
        {
            to = to == null ? new TableOptions() : to;
            var Filters = to.Filters?.Where(x => x.Value != null && x.Value.ToString() != "").ToList();
            string SortName = to.SortName;
            string SortDirection = to.SortDirection;
            int? Page = to.Page;
            int? CountPerPage = to.Count;
            var matchCollection = Regex.Matches(Query, @"\{[0-9^]+\}");
            //int paramCount = (matchCollection.Count == 0 ? 0 : matchCollection.Cast<Match>().Select(x => x.Value.Replace("{", "").Replace("}", "").ToInt32()).ToList()[matchCollection.Count - 1] + 1);
            int paramCount = matchCollection.Count == 0 ? 0 : matchCollection.Cast<Match>().Select(x => x.Value.Replace("{", "").Replace("}", "").ToInt32()).ToList().Max() + 1;
            var filterParams = new List<object>();
            string filter = "";

            if (Filters != null)
                if (Filters.Count > 0)
                {
                    Filters.Map(x =>
                    {
                        if (x.Type == FilterType.IN || x.Type == FilterType.NotIN || x.Type == FilterType.Between)
                        {
                            if (x.Type == FilterType.Between)
                            {
                                var val = new List<string>();
                                foreach (var d in x.Value.ToObject<List<string>>()) val.Add(d.IsNull("").ToString());
                                if (val[0] == "") val[0] = val[1];
                                if (val[1] == "") val[1] = val[0];
                                if (val[0] == "" && val[1] == "") return;
                                filterParams.Add(Convert.ToDateTime(val[0]));
                                filterParams.Add(Convert.ToDateTime(val[1]));
                            }
                            else
                                foreach (var xx in x.Value.ToString().Split(","))
                                {
                                    filterParams.Add(xx);
                                }
                        }
                        else
                        {
                            filterParams.Add(x.Value);
                        }
                    });
                    filter = Filter.BuildStringFilter(Filters, paramCount);
                }
            var nParam = new object[filterParams.Count];
            nParam = filterParams.ToArray();

            var extendedParams = new object[parameters.Length + nParam.Length];
            parameters.CopyTo(extendedParams, 0);
            nParam.CopyTo(extendedParams, parameters.Length);

            var Count = source.Query<CountData>().AsNoTracking().FromSql($"SELECT COUNT(1) Cnt FROM {Query} {filter}", extendedParams).FirstOrDefault().Cnt;
            var clsType = typeof(T).IsDefined(typeof(TableAttribute), false);

            IQueryable<T> Data = null;
            if (clsType)
                Data = source.Set<T>().AsNoTracking().FromSql($"SELECT * FROM {Query} {filter} {(SortName.IsNull("").ToString() != "" ? $"ORDER BY {SortName} {SortDirection}" : "")} {(Page != null ? $"OFFSET {((Page - 1) * CountPerPage).ToString()} ROWS FETCH NEXT {CountPerPage.ToString()} ROWS ONLY" : "")}", extendedParams);
            else
                Data = source.Query<T>().AsNoTracking().FromSql($"SELECT * FROM {Query} {filter} {(SortName.IsNull("").ToString() != "" ? $"ORDER BY {SortName} {SortDirection}" : "")} {(Page != null ? $"OFFSET {((Page - 1) * CountPerPage).ToString()} ROWS FETCH NEXT {CountPerPage.ToString()} ROWS ONLY" : "")}", extendedParams);

            return new TableData<T>()
            {
                Data = Data,
                Count = Count
            };
        }
        public static IEnumerable<T> ExecQuery<T>(this DbContext source, string Query, params object[] parameters) where T : class
        {
            var clsType = typeof(T).IsDefined(typeof(TableAttribute), false);
            if (clsType)
                return source.Set<T>().AsNoTracking().FromSql(Query, parameters);
            else
                return source.Query<T>().AsNoTracking().FromSql(Query, parameters);
        }
        public static T Single<T>(this DbContext source, string Query, params object[] parameters) where T : class
        {
            var clsType = typeof(T).IsDefined(typeof(TableAttribute), false);
            if (clsType)
                return source.Set<T>().AsNoTracking().FromSql(Query, parameters).FirstOrDefault();
            else
                return source.Query<T>().AsNoTracking().FromSql(Query, parameters).FirstOrDefault();

        }
        public static bool Any(this DbContext source, string Query, params object[] parameters)
        {
            return source.Query<CountData>().AsNoTracking().FromSql($"SELECT COUNT(1) Cnt FROM {Query}", parameters).FirstOrDefault().Cnt > 0 ? true : false;
        }
        public static void ExecNonQuery(this DbContext source, string Query, params object[] parameters)
        {
            source.Database.ExecuteSqlCommand(Query, parameters);
        }
        public static string ToListArray<T>(this IEnumerable<T> source, string column)
        {
            var x = Expression.Parameter(typeof(T), "x");
            var body = Expression.PropertyOrField(x, column);
            var convert = Expression.Convert(body, typeof(object));
            var lambda = Expression.Lambda<Func<T, object>>(convert, x);
            if (source.Count() > 0) return string.Join(",", source.Select(lambda.Compile()).ToList());
            else return "";
        }
        public static int ExecScalarInt(this DbContext source, string Query, params object[] parameters)
        {
            int val = source.Single<IntReturn>(Query, parameters) == null ? 0 : source.Single<IntReturn>(Query, parameters).Value.ToInt32();
            return val;
        }
        public static string ExecScalarString(this DbContext source, string Query, params object[] parameters)
        {
            string val = source.Single<StringReturn>(Query, parameters) == null ? null : source.Single<StringReturn>(Query, parameters).Value;
            return val;
        }
        public static bool ExecScalarBoolean(this DbContext source, string Query, params object[] parameters)
        {
            bool val = source.Single<BooleanReturn>(Query, parameters) == null ? false : Convert.ToBoolean(source.Single<BooleanReturn>(Query, parameters).Value);
            return val;
        }
        public static DateTime ExecScalarDateTime(this DbContext source, string Query, params object[] parameters)
        {
            DateTime val = source.Single<DateTimeReturn>(Query, parameters) == null ? DateTime.Now : Convert.ToDateTime(source.Single<DateTimeReturn>(Query, parameters).Value);
            return val;
        }
        public static decimal ExecScalarDecimal(this DbContext source, string Query, params object[] parameters)
        {
            decimal val = source.Single<DecimalReturn>(Query, parameters) == null ? Convert.ToDecimal(0) : Convert.ToDecimal(source.Single<DecimalReturn>(Query, parameters).Value);
            return val;
        }
        public static string StripSlashes(this object str)
        {
            return str.ToString().Trim(new char[] { '\'', '\r', '\n', '\t' });
        }
        public static void Set(this ISession session, string key, object value)
        {
            if (value == null)
            {
                session.Set(key, new byte[] { });
                return;
            }
            else
            {
                if (value.GetType().IsBuiltIn())
                    session.Set(key, Encoding.UTF8.GetBytes(value.ToString()));
                else
                    session.Set(key, Encoding.UTF8.GetBytes(value.ToJson()));
            }
        }
        public static T Get<T>(this ISession session, string key)
        {
            if (session.Keys.Count() == 0) throw new Exception("Session Expired.");
            if (session.Keys.Contains(key))
            {
                var gg = session.Get(key);

                if (gg.Length == 0)
                    return default(T);

                if (typeof(T).IsBuiltIn())
                    return (T)Convert.ChangeType(Encoding.UTF8.GetString(gg), typeof(T));
                else
                    return Encoding.UTF8.GetString(gg).ToObject<T>();

            }
            else
                throw new KeyNotFoundException();
        }
        public static object GetSession(this ISession session, string key)
        {
            var j = default(object);
            if (session.Keys.Contains(key))
            {
                var d = Encoding.UTF8.GetString(session.Get(key));

                if (d.IsJson() || d.IsJsonArray())
                    j = d.ToObject<dynamic>();
                else
                    j = d;
            }
            else
                throw new KeyNotFoundException();

            return j;
        }
        public static string BuildParameter(this string Query, TableOptions tblOptions, params object[] parameters)
        {
            tblOptions = tblOptions == null ? new TableOptions() : tblOptions;
            var Filters = tblOptions.Filters?.Where(x => x.Value != null && x.Value.ToString() != "").ToList();
            string SortName = tblOptions.SortName;
            string SortDirection = tblOptions.SortDirection;
            int? Page = tblOptions.Page;
            int? CountPerPage = tblOptions.Count;
            var matchCollection = Regex.Matches(Query, @"\{[0-9^]+\}");
            //int paramCount = (matchCollection.Count == 0 ? 0 : matchCollection.Cast<Match>().Select(x => x.Value.Replace("{", "").Replace("}", "").ToInt32()).ToList()[matchCollection.Count - 1] + 1);
            int paramCount = matchCollection.Count == 0 ? 0 : matchCollection.Cast<Match>().Select(x => x.Value.Replace("{", "").Replace("}", "").ToInt32()).Max() + 1;
            var filterParams = new List<object>();
            string filter = "";

            if (Filters != null)
                if (Filters.Count > 0)
                {
                    Filters.Map(x =>
                    {
                        if (x.Type == FilterType.IN || x.Type == FilterType.NotIN || x.Type == FilterType.Between)
                        {
                            if (x.Type == FilterType.Between)
                            {
                                var val = new List<string>();
                                foreach (var d in x.Value.ToObject<List<string>>()) val.Add(d.IsNull("").ToString());
                                if (val[0] == "") val[0] = val[1];
                                if (val[1] == "") val[1] = val[0];
                                if (val[0] == "" && val[1] == "") return;
                                filterParams.Add(Convert.ToDateTime(val[0]));
                                filterParams.Add(Convert.ToDateTime(val[1]));
                            }
                            else
                                foreach (var xx in x.Value.ToString().Split(","))
                                {
                                    filterParams.Add(xx);
                                }
                        }
                        else
                        {
                            filterParams.Add(x.Value);
                        }
                    });
                    filter = Filter.BuildStringFilter(Filters, paramCount);
                }
            var nParam = new object[filterParams.Count];
            nParam = filterParams.ToArray();

            var extendedParams = new object[parameters.Length + nParam.Length];
            parameters.CopyTo(extendedParams, 0);
            nParam.CopyTo(extendedParams, parameters.Length);

            var idx = 0;
            Query = $"select * from ({Query})a {filter}";
            foreach (object value in extendedParams)
            {
                Query = Query.Replace(matchCollection[idx].Value, $"'{value}'");
                idx += 1;
            }

            return Query;
        }
        public static int ToDatetimeMinute(this DateTime datetime, DateTime? refDate)
        {
            return (datetime.Subtract(refDate.IsNull(DateTime.Parse(datetime.ToString("MM/dd/yyyy"))).ToDate()).TotalHours * 60).ToInt32();
        }
        public static string GetContentType(this string FileName, string AlternateExtension = "")
        {
            return new ContentType().GetContentType(Path.GetExtension(FileName), AlternateExtension);
        }
    }
    public class TableData<T>
    {
        public IQueryable<T> Data { get; set; }
        public int Count { get; set; }
    }
    [NotMapped]
    public class CountData
    {
        public int Cnt { get; set; }
    }
    [NotMapped]
    public class IntReturn
    {
        public int? Value { get; set; }
    }
    [NotMapped]
    public class StringReturn
    {
        public string Value { get; set; }
    }
    [NotMapped]
    public class BooleanReturn
    {
        public bool? Value { get; set; }
    }
    [NotMapped]
    public class DateTimeReturn
    {
        public DateTime? Value { get; set; }
    }
    [NotMapped]
    public class DecimalReturn
    {
        public decimal? Value { get; set; }
    }
    public class FileContext
    {
        public byte[] DataBytes { get; set; }
        public string Filename { get; set; }
        public string DataString64 { get; set; }
        public string ContentType { get; set; }
    }

    public class ContentType : Dictionary<string, string>
    {
        public ContentType()
        {
            this.Add(".x3d", "application/vnd.hzn-3d-crossword");
            this.Add(".3gp", "video/3gpp");
            this.Add(".3g2", "video/3gpp2");
            this.Add(".mseq", "application/vnd.mseq");
            this.Add(".pwn", "application/vnd.3m.post-it-notes");
            this.Add(".plb", "application/vnd.3gpp.pic-bw-large");
            this.Add(".psb", "application/vnd.3gpp.pic-bw-small");
            this.Add(".pvb", "application/vnd.3gpp.pic-bw-var");
            this.Add(".tcap", "application/vnd.3gpp2.tcap");
            this.Add(".7z", "application/x-7z-compressed");
            this.Add(".abw", "application/x-abiword");
            this.Add(".ace", "application/x-ace-compressed");
            this.Add(".acc", "application/vnd.americandynamics.acc");
            this.Add(".acu", "application/vnd.acucobol");
            this.Add(".atc", "application/vnd.acucorp");
            this.Add(".adp", "audio/adpcm");
            this.Add(".aab", "application/x-authorware-bin");
            this.Add(".aam", "application/x-authorware-map");
            this.Add(".aas", "application/x-authorware-seg");
            this.Add(".air", "application/vnd.adobe.air-application-installer-package+zip");
            this.Add(".swf", "application/x-shockwave-flash");
            this.Add(".fxp", "application/vnd.adobe.fxp");
            this.Add(".pdf", "application/pdf");
            this.Add(".ppd", "application/vnd.cups-ppd");
            this.Add(".dir", "application/x-director");
            this.Add(".xdp", "application/vnd.adobe.xdp+xml");
            this.Add(".xfdf", "application/vnd.adobe.xfdf");
            this.Add(".aac", "audio/x-aac");
            this.Add(".ahead", "application/vnd.ahead.space");
            this.Add(".azf", "application/vnd.airzip.filesecure.azf");
            this.Add(".azs", "application/vnd.airzip.filesecure.azs");
            this.Add(".azw", "application/vnd.amazon.ebook");
            this.Add(".ami", "application/vnd.amiga.ami");
            this.Add("N/A", "application/andrew-inset");
            this.Add(".apk", "application/vnd.android.package-archive");
            this.Add(".cii", "application/vnd.anser-web-certificate-issue-initiation");
            this.Add(".fti", "application/vnd.anser-web-funds-transfer-initiation");
            this.Add(".atx", "application/vnd.antix.game-component");
            this.Add(".mpkg", "application/vnd.apple.installer+xml");
            this.Add(".aw", "application/applixware");
            this.Add(".les", "application/vnd.hhe.lesson-player");
            this.Add(".swi", "application/vnd.aristanetworks.swi");
            this.Add(".s", "text/x-asm");
            this.Add(".atomcat", "application/atomcat+xml");
            this.Add(".atomsvc", "application/atomsvc+xml");
            this.Add(".atom, .xml", "application/atom+xml");
            this.Add(".ac", "application/pkix-attr-cert");
            this.Add(".aif", "audio/x-aiff");
            this.Add(".avi", "video/x-msvideo");
            this.Add(".aep", "application/vnd.audiograph");
            this.Add(".dxf", "image/vnd.dxf");
            this.Add(".dwf", "model/vnd.dwf");
            this.Add(".par", "text/plain-bas");
            this.Add(".bcpio", "application/x-bcpio");
            this.Add(".bin", "application/octet-stream");
            this.Add(".bmp", "image/bmp");
            this.Add(".torrent", "application/x-bittorrent");
            this.Add(".cod", "application/vnd.rim.cod");
            this.Add(".mpm", "application/vnd.blueice.multipass");
            this.Add(".bmi", "application/vnd.bmi");
            this.Add(".sh", "application/x-sh");
            this.Add(".btif", "image/prs.btif");
            this.Add(".rep", "application/vnd.businessobjects");
            this.Add(".bz", "application/x-bzip");
            this.Add(".bz2", "application/x-bzip2");
            this.Add(".csh", "application/x-csh");
            this.Add(".c", "text/x-c");
            this.Add(".cdxml", "application/vnd.chemdraw+xml");
            this.Add(".css", "text/css");
            this.Add(".cdx", "chemical/x-cdx");
            this.Add(".cml", "chemical/x-cml");
            this.Add(".csml", "chemical/x-csml");
            this.Add(".cdbcmsg", "application/vnd.contact.cmsg");
            this.Add(".cla", "application/vnd.claymore");
            this.Add(".c4g", "application/vnd.clonk.c4group");
            this.Add(".sub", "image/vnd.dvb.subtitle");
            this.Add(".cdmia", "application/cdmi-capability");
            this.Add(".cdmic", "application/cdmi-container");
            this.Add(".cdmid", "application/cdmi-domain");
            this.Add(".cdmio", "application/cdmi-object");
            this.Add(".cdmiq", "application/cdmi-queue");
            this.Add(".c11amc", "application/vnd.cluetrust.cartomobile-config");
            this.Add(".c11amz", "application/vnd.cluetrust.cartomobile-config-pkg");
            this.Add(".ras", "image/x-cmu-raster");
            this.Add(".dae", "model/vnd.collada+xml");
            this.Add(".csv", "text/csv");
            this.Add(".cpt", "application/mac-compactpro");
            this.Add(".wmlc", "application/vnd.wap.wmlc");
            this.Add(".cgm", "image/cgm");
            this.Add(".ice", "x-conference/x-cooltalk");
            this.Add(".cmx", "image/x-cmx");
            this.Add(".xar", "application/vnd.xara");
            this.Add(".cmc", "application/vnd.cosmocaller");
            this.Add(".cpio", "application/x-cpio");
            this.Add(".clkx", "application/vnd.crick.clicker");
            this.Add(".clkk", "application/vnd.crick.clicker.keyboard");
            this.Add(".clkp", "application/vnd.crick.clicker.palette");
            this.Add(".clkt", "application/vnd.crick.clicker.template");
            this.Add(".clkw", "application/vnd.crick.clicker.wordbank");
            this.Add(".wbs", "application/vnd.criticaltools.wbs+xml");
            this.Add(".cryptonote", "application/vnd.rig.cryptonote");
            this.Add(".cif", "chemical/x-cif");
            this.Add(".cmdf", "chemical/x-cmdf");
            this.Add(".cu", "application/cu-seeme");
            this.Add(".cww", "application/prs.cww");
            this.Add(".curl", "text/vnd.curl");
            this.Add(".dcurl", "text/vnd.curl.dcurl");
            this.Add(".mcurl", "text/vnd.curl.mcurl");
            this.Add(".scurl", "text/vnd.curl.scurl");
            this.Add(".car", "application/vnd.curl.car");
            this.Add(".pcurl", "application/vnd.curl.pcurl");
            this.Add(".cmp", "application/vnd.yellowriver-custom-menu");
            this.Add(".dssc", "application/dssc+der");
            this.Add(".xdssc", "application/dssc+xml");
            this.Add(".deb", "application/x-debian-package");
            this.Add(".uva", "audio/vnd.dece.audio");
            this.Add(".uvi", "image/vnd.dece.graphic");
            this.Add(".uvh", "video/vnd.dece.hd");
            this.Add(".uvm", "video/vnd.dece.mobile");
            this.Add(".uvu", "video/vnd.uvvu.mp4");
            this.Add(".uvp", "video/vnd.dece.pd");
            this.Add(".uvs", "video/vnd.dece.sd");
            this.Add(".uvv", "video/vnd.dece.video");
            this.Add(".dvi", "application/x-dvi");
            this.Add(".seed", "application/vnd.fdsn.seed");
            this.Add(".dtb", "application/x-dtbook+xml");
            this.Add(".res", "application/x-dtbresource+xml");
            this.Add(".ait", "application/vnd.dvb.ait");
            this.Add(".svc", "application/vnd.dvb.service");
            this.Add(".eol", "audio/vnd.digital-winds");
            this.Add(".djvu", "image/vnd.djvu");
            this.Add(".dtd", "application/xml-dtd");
            this.Add(".mlp", "application/vnd.dolby.mlp");
            this.Add(".wad", "application/x-doom");
            this.Add(".dpg", "application/vnd.dpgraph");
            this.Add(".dra", "audio/vnd.dra");
            this.Add(".dfac", "application/vnd.dreamfactory");
            this.Add(".dts", "audio/vnd.dts");
            this.Add(".dtshd", "audio/vnd.dts.hd");
            this.Add(".dwg", "image/vnd.dwg");
            this.Add(".geo", "application/vnd.dynageo");
            this.Add(".es", "application/ecmascript");
            this.Add(".mag", "application/vnd.ecowin.chart");
            this.Add(".mmr", "image/vnd.fujixerox.edmics-mmr");
            this.Add(".rlc", "image/vnd.fujixerox.edmics-rlc");
            this.Add(".exi", "application/exi");
            this.Add(".mgz", "application/vnd.proteus.magazine");
            this.Add(".epub", "application/epub+zip");
            this.Add(".eml", "message/rfc822");
            this.Add(".nml", "application/vnd.enliven");
            this.Add(".xpr", "application/vnd.is-xpr");
            this.Add(".xif", "image/vnd.xiff");
            this.Add(".xfdl", "application/vnd.xfdl");
            this.Add(".emma", "application/emma+xml");
            this.Add(".ez2", "application/vnd.ezpix-album");
            this.Add(".ez3", "application/vnd.ezpix-package");
            this.Add(".fst", "image/vnd.fst");
            this.Add(".fvt", "video/vnd.fvt");
            this.Add(".fbs", "image/vnd.fastbidsheet");
            this.Add(".fe_launch", "application/vnd.denovo.fcselayout-link");
            this.Add(".f4v", "video/x-f4v");
            this.Add(".flv", "video/x-flv");
            this.Add(".fpx", "image/vnd.fpx");
            this.Add(".npx", "image/vnd.net-fpx");
            this.Add(".flx", "text/vnd.fmi.flexstor");
            this.Add(".fli", "video/x-fli");
            this.Add(".ftc", "application/vnd.fluxtime.clip");
            this.Add(".fdf", "application/vnd.fdf");
            this.Add(".f", "text/x-fortran");
            this.Add(".mif", "application/vnd.mif");
            this.Add(".fm", "application/vnd.framemaker");
            this.Add(".fh", "image/x-freehand");
            this.Add(".fsc", "application/vnd.fsc.weblaunch");
            this.Add(".fnc", "application/vnd.frogans.fnc");
            this.Add(".ltf", "application/vnd.frogans.ltf");
            this.Add(".ddd", "application/vnd.fujixerox.ddd");
            this.Add(".xdw", "application/vnd.fujixerox.docuworks");
            this.Add(".xbd", "application/vnd.fujixerox.docuworks.binder");
            this.Add(".oas", "application/vnd.fujitsu.oasys");
            this.Add(".oa2", "application/vnd.fujitsu.oasys2");
            this.Add(".oa3", "application/vnd.fujitsu.oasys3");
            this.Add(".fg5", "application/vnd.fujitsu.oasysgp");
            this.Add(".bh2", "application/vnd.fujitsu.oasysprs");
            this.Add(".spl", "application/x-futuresplash");
            this.Add(".fzs", "application/vnd.fuzzysheet");
            this.Add(".g3", "image/g3fax");
            this.Add(".gmx", "application/vnd.gmx");
            this.Add(".gtw", "model/vnd.gtw");
            this.Add(".txd", "application/vnd.genomatix.tuxedo");
            this.Add(".ggb", "application/vnd.geogebra.file");
            this.Add(".ggt", "application/vnd.geogebra.tool");
            this.Add(".gdl", "model/vnd.gdl");
            this.Add(".gex", "application/vnd.geometry-explorer");
            this.Add(".gxt", "application/vnd.geonext");
            this.Add(".g2w", "application/vnd.geoplan");
            this.Add(".g3w", "application/vnd.geospace");
            this.Add(".gsf", "application/x-font-ghostscript");
            this.Add(".bdf", "application/x-font-bdf");
            this.Add(".gtar", "application/x-gtar");
            this.Add(".texinfo", "application/x-texinfo");
            this.Add(".gnumeric", "application/x-gnumeric");
            this.Add(".kml", "application/vnd.google-earth.kml+xml");
            this.Add(".kmz", "application/vnd.google-earth.kmz");
            this.Add(".gqf", "application/vnd.grafeq");
            this.Add(".gif", "image/gif");
            this.Add(".gv", "text/vnd.graphviz");
            this.Add(".gac", "application/vnd.groove-account");
            this.Add(".ghf", "application/vnd.groove-help");
            this.Add(".gim", "application/vnd.groove-identity-message");
            this.Add(".grv", "application/vnd.groove-injector");
            this.Add(".gtm", "application/vnd.groove-tool-message");
            this.Add(".tpl", "application/vnd.groove-tool-template");
            this.Add(".vcg", "application/vnd.groove-vcard");
            this.Add(".h261", "video/h261");
            this.Add(".h263", "video/h263");
            this.Add(".h264", "video/h264");
            this.Add(".hpid", "application/vnd.hp-hpid");
            this.Add(".hps", "application/vnd.hp-hps");
            this.Add(".hdf", "application/x-hdf");
            this.Add(".rip", "audio/vnd.rip");
            this.Add(".hbci", "application/vnd.hbci");
            this.Add(".jlt", "application/vnd.hp-jlyt");
            this.Add(".pcl", "application/vnd.hp-pcl");
            this.Add(".hpgl", "application/vnd.hp-hpgl");
            this.Add(".hvs", "application/vnd.yamaha.hv-script");
            this.Add(".hvd", "application/vnd.yamaha.hv-dic");
            this.Add(".hvp", "application/vnd.yamaha.hv-voice");
            this.Add(".sfd-hdstx", "application/vnd.hydrostatix.sof-data");
            this.Add(".stk", "application/hyperstudio");
            this.Add(".hal", "application/vnd.hal+xml");
            this.Add(".html", "text/html");
            this.Add(".irm", "application/vnd.ibm.rights-management");
            this.Add(".sc", "application/vnd.ibm.secure-container");
            this.Add(".ics", "text/calendar");
            this.Add(".icc", "application/vnd.iccprofile");
            this.Add(".ico", "image/x-icon");
            this.Add(".igl", "application/vnd.igloader");
            this.Add(".ief", "image/ief");
            this.Add(".ivp", "application/vnd.immervision-ivp");
            this.Add(".ivu", "application/vnd.immervision-ivu");
            this.Add(".rif", "application/reginfo+xml");
            this.Add(".3dml", "text/vnd.in3d.3dml");
            this.Add(".spot", "text/vnd.in3d.spot");
            this.Add(".igs", "model/iges");
            this.Add(".i2g", "application/vnd.intergeo");
            this.Add(".cdy", "application/vnd.cinderella");
            this.Add(".xpw", "application/vnd.intercon.formnet");
            this.Add(".fcs", "application/vnd.isac.fcs");
            this.Add(".ipfix", "application/ipfix");
            this.Add(".cer", "application/pkix-cert");
            this.Add(".pki", "application/pkixcmp");
            this.Add(".crl", "application/pkix-crl");
            this.Add(".pkipath", "application/pkix-pkipath");
            this.Add(".igm", "application/vnd.insors.igm");
            this.Add(".rcprofile", "application/vnd.ipunplugged.rcprofile");
            this.Add(".irp", "application/vnd.irepository.package+xml");
            this.Add(".jad", "text/vnd.sun.j2me.app-descriptor");
            this.Add(".jar", "application/java-archive");
            this.Add(".class", "application/java-vm");
            this.Add(".jnlp", "application/x-java-jnlp-file");
            this.Add(".ser", "application/java-serialized-object");
            this.Add(".java", "text/x-java-source,java");
            this.Add(".js", "application/javascript");
            this.Add(".json", "application/json");
            this.Add(".joda", "application/vnd.joost.joda-archive");
            this.Add(".jpm", "video/jpm");
            this.Add(".jpeg", "image/jpeg");
            this.Add(".jpg", "image/jpeg");
            this.Add(".jpgv", "video/jpeg");
            this.Add(".ktz", "application/vnd.kahootz");
            this.Add(".mmd", "application/vnd.chipnuts.karaoke-mmd");
            this.Add(".karbon", "application/vnd.kde.karbon");
            this.Add(".chrt", "application/vnd.kde.kchart");
            this.Add(".kfo", "application/vnd.kde.kformula");
            this.Add(".flw", "application/vnd.kde.kivio");
            this.Add(".kon", "application/vnd.kde.kontour");
            this.Add(".kpr", "application/vnd.kde.kpresenter");
            this.Add(".ksp", "application/vnd.kde.kspread");
            this.Add(".kwd", "application/vnd.kde.kword");
            this.Add(".htke", "application/vnd.kenameaapp");
            this.Add(".kia", "application/vnd.kidspiration");
            this.Add(".kne", "application/vnd.kinar");
            this.Add(".sse", "application/vnd.kodak-descriptor");
            this.Add(".lasxml", "application/vnd.las.las+xml");
            this.Add(".latex", "application/x-latex");
            this.Add(".lbd", "application/vnd.llamagraphics.life-balance.desktop");
            this.Add(".lbe", "application/vnd.llamagraphics.life-balance.exchange+xml");
            this.Add(".jam", "application/vnd.jam");
            this.Add("0.123", "application/vnd.lotus-1-2-3");
            this.Add(".apr", "application/vnd.lotus-approach");
            this.Add(".pre", "application/vnd.lotus-freelance");
            this.Add(".nsf", "application/vnd.lotus-notes");
            this.Add(".org", "application/vnd.lotus-organizer");
            this.Add(".scm", "application/vnd.lotus-screencam");
            this.Add(".lwp", "application/vnd.lotus-wordpro");
            this.Add(".lvp", "audio/vnd.lucent.voice");
            this.Add(".m3u", "audio/x-mpegurl");
            this.Add(".m4v", "video/x-m4v");
            this.Add(".hqx", "application/mac-binhex40");
            this.Add(".portpkg", "application/vnd.macports.portpkg");
            this.Add(".mgp", "application/vnd.osgeo.mapguide.package");
            this.Add(".mrc", "application/marc");
            this.Add(".mrcx", "application/marcxml+xml");
            this.Add(".mxf", "application/mxf");
            this.Add(".nbp", "application/vnd.wolfram.player");
            this.Add(".ma", "application/mathematica");
            this.Add(".mathml", "application/mathml+xml");
            this.Add(".mbox", "application/mbox");
            this.Add(".mc1", "application/vnd.medcalcdata");
            this.Add(".mscml", "application/mediaservercontrol+xml");
            this.Add(".cdkey", "application/vnd.mediastation.cdkey");
            this.Add(".mwf", "application/vnd.mfer");
            this.Add(".mfm", "application/vnd.mfmp");
            this.Add(".msh", "model/mesh");
            this.Add(".mads", "application/mads+xml");
            this.Add(".mets", "application/mets+xml");
            this.Add(".mods", "application/mods+xml");
            this.Add(".meta4", "application/metalink4+xml");
            this.Add(".potm", "application/vnd.ms-powerpoint.template.macroenabled.12");
            this.Add(".docm", "application/vnd.ms-word.document.macroenabled.12");
            this.Add(".dotm", "application/vnd.ms-word.template.macroenabled.12");
            this.Add(".mcd", "application/vnd.mcd");
            this.Add(".flo", "application/vnd.micrografx.flo");
            this.Add(".igx", "application/vnd.micrografx.igx");
            this.Add(".es3", "application/vnd.eszigno3+xml");
            this.Add(".mdb", "application/x-msaccess");
            this.Add(".asf", "video/x-ms-asf");
            this.Add(".exe", "application/x-msdownload");
            this.Add(".cil", "application/vnd.ms-artgalry");
            this.Add(".cab", "application/vnd.ms-cab-compressed");
            this.Add(".ims", "application/vnd.ms-ims");
            this.Add(".application", "application/x-ms-application");
            this.Add(".clp", "application/x-msclip");
            this.Add(".mdi", "image/vnd.ms-modi");
            this.Add(".eot", "application/vnd.ms-fontobject");
            this.Add(".xls", "application/vnd.ms-excel");
            this.Add(".xlam", "application/vnd.ms-excel.addin.macroenabled.12");
            this.Add(".xlsb", "application/vnd.ms-excel.sheet.binary.macroenabled.12");
            this.Add(".xltm", "application/vnd.ms-excel.template.macroenabled.12");
            this.Add(".xlsm", "application/vnd.ms-excel.sheet.macroenabled.12");
            this.Add(".chm", "application/vnd.ms-htmlhelp");
            this.Add(".crd", "application/x-mscardfile");
            this.Add(".lrm", "application/vnd.ms-lrm");
            this.Add(".mvb", "application/x-msmediaview");
            this.Add(".mny", "application/x-msmoney");
            this.Add(".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation");
            this.Add(".sldx", "application/vnd.openxmlformats-officedocument.presentationml.slide");
            this.Add(".ppsx", "application/vnd.openxmlformats-officedocument.presentationml.slideshow");
            this.Add(".potx", "application/vnd.openxmlformats-officedocument.presentationml.template");
            this.Add(".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            this.Add(".xltx", "application/vnd.openxmlformats-officedocument.spreadsheetml.template");
            this.Add(".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            this.Add(".dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template");
            this.Add(".obd", "application/x-msbinder");
            this.Add(".thmx", "application/vnd.ms-officetheme");
            this.Add(".onetoc", "application/onenote");
            this.Add(".pya", "audio/vnd.ms-playready.media.pya");
            this.Add(".pyv", "video/vnd.ms-playready.media.pyv");
            this.Add(".ppt", "application/vnd.ms-powerpoint");
            this.Add(".ppam", "application/vnd.ms-powerpoint.addin.macroenabled.12");
            this.Add(".sldm", "application/vnd.ms-powerpoint.slide.macroenabled.12");
            this.Add(".pptm", "application/vnd.ms-powerpoint.presentation.macroenabled.12");
            this.Add(".ppsm", "application/vnd.ms-powerpoint.slideshow.macroenabled.12");
            this.Add(".mpp", "application/vnd.ms-project");
            this.Add(".pub", "application/x-mspublisher");
            this.Add(".scd", "application/x-msschedule");
            this.Add(".xap", "application/x-silverlight-app");
            this.Add(".stl", "application/vnd.ms-pki.stl");
            this.Add(".cat", "application/vnd.ms-pki.seccat");
            this.Add(".vsd", "application/vnd.visio");
            this.Add(".wm", "video/x-ms-wm");
            this.Add(".wma", "audio/x-ms-wma");
            this.Add(".wax", "audio/x-ms-wax");
            this.Add(".wmx", "video/x-ms-wmx");
            this.Add(".wmd", "application/x-ms-wmd");
            this.Add(".wpl", "application/vnd.ms-wpl");
            this.Add(".wmz", "application/x-ms-wmz");
            this.Add(".wmv", "video/x-ms-wmv");
            this.Add(".wvx", "video/x-ms-wvx");
            this.Add(".wmf", "application/x-msmetafile");
            this.Add(".trm", "application/x-msterminal");
            this.Add(".doc", "application/msword");
            this.Add(".wri", "application/x-mswrite");
            this.Add(".wps", "application/vnd.ms-works");
            this.Add(".xbap", "application/x-ms-xbap");
            this.Add(".xps", "application/vnd.ms-xpsdocument");
            this.Add(".mid", "audio/midi");
            this.Add(".mpy", "application/vnd.ibm.minipay");
            this.Add(".afp", "application/vnd.ibm.modcap");
            this.Add(".rms", "application/vnd.jcp.javame.midlet-rms");
            this.Add(".tmo", "application/vnd.tmobile-livetv");
            this.Add(".prc", "application/x-mobipocket-ebook");
            this.Add(".mbk", "application/vnd.mobius.mbk");
            this.Add(".dis", "application/vnd.mobius.dis");
            this.Add(".plc", "application/vnd.mobius.plc");
            this.Add(".mqy", "application/vnd.mobius.mqy");
            this.Add(".msl", "application/vnd.mobius.msl");
            this.Add(".txf", "application/vnd.mobius.txf");
            this.Add(".daf", "application/vnd.mobius.daf");
            this.Add(".fly", "text/vnd.fly");
            this.Add(".mpc", "application/vnd.mophun.certificate");
            this.Add(".mpn", "application/vnd.mophun.application");
            this.Add(".mj2", "video/mj2");
            this.Add(".mpga", "audio/mpeg");
            this.Add(".mxu", "video/vnd.mpegurl");
            this.Add(".mpeg", "video/mpeg");
            this.Add(".m21", "application/mp21");
            this.Add(".mp4a", "audio/mp4");
            this.Add(".mp4", "video/mp4");
            // this.Add(".mp4", "application/mp4");
            this.Add(".m3u8", "application/vnd.apple.mpegurl");
            this.Add(".mus", "application/vnd.musician");
            this.Add(".msty", "application/vnd.muvee.style");
            this.Add(".mxml", "application/xv+xml");
            this.Add(".ngdat", "application/vnd.nokia.n-gage.data");
            this.Add(".n-gage", "application/vnd.nokia.n-gage.symbian.install");
            this.Add(".ncx", "application/x-dtbncx+xml");
            this.Add(".nc", "application/x-netcdf");
            this.Add(".nlu", "application/vnd.neurolanguage.nlu");
            this.Add(".dna", "application/vnd.dna");
            this.Add(".nnd", "application/vnd.noblenet-directory");
            this.Add(".nns", "application/vnd.noblenet-sealer");
            this.Add(".nnw", "application/vnd.noblenet-web");
            this.Add(".rpst", "application/vnd.nokia.radio-preset");
            this.Add(".rpss", "application/vnd.nokia.radio-presets");
            this.Add(".n3", "text/n3");
            this.Add(".edm", "application/vnd.novadigm.edm");
            this.Add(".edx", "application/vnd.novadigm.edx");
            this.Add(".ext", "application/vnd.novadigm.ext");
            this.Add(".gph", "application/vnd.flographit");
            this.Add(".ecelp4800", "audio/vnd.nuera.ecelp4800");
            this.Add(".ecelp7470", "audio/vnd.nuera.ecelp7470");
            this.Add(".ecelp9600", "audio/vnd.nuera.ecelp9600");
            this.Add(".oda", "application/oda");
            this.Add(".ogx", "application/ogg");
            this.Add(".oga", "audio/ogg");
            this.Add(".ogv", "video/ogg");
            this.Add(".dd2", "application/vnd.oma.dd2+xml");
            this.Add(".oth", "application/vnd.oasis.opendocument.text-web");
            this.Add(".opf", "application/oebps-package+xml");
            this.Add(".qbo", "application/vnd.intu.qbo");
            this.Add(".oxt", "application/vnd.openofficeorg.extension");
            this.Add(".osf", "application/vnd.yamaha.openscoreformat");
            this.Add(".weba", "audio/webm");
            this.Add(".webm", "video/webm");
            this.Add(".odc", "application/vnd.oasis.opendocument.chart");
            this.Add(".otc", "application/vnd.oasis.opendocument.chart-template");
            this.Add(".odb", "application/vnd.oasis.opendocument.database");
            this.Add(".odf", "application/vnd.oasis.opendocument.formula");
            this.Add(".odft", "application/vnd.oasis.opendocument.formula-template");
            this.Add(".odg", "application/vnd.oasis.opendocument.graphics");
            this.Add(".otg", "application/vnd.oasis.opendocument.graphics-template");
            this.Add(".odi", "application/vnd.oasis.opendocument.image");
            this.Add(".oti", "application/vnd.oasis.opendocument.image-template");
            this.Add(".odp", "application/vnd.oasis.opendocument.presentation");
            this.Add(".otp", "application/vnd.oasis.opendocument.presentation-template");
            this.Add(".ods", "application/vnd.oasis.opendocument.spreadsheet");
            this.Add(".ots", "application/vnd.oasis.opendocument.spreadsheet-template");
            this.Add(".odt", "application/vnd.oasis.opendocument.text");
            this.Add(".odm", "application/vnd.oasis.opendocument.text-master");
            this.Add(".ott", "application/vnd.oasis.opendocument.text-template");
            this.Add(".ktx", "image/ktx");
            this.Add(".sxc", "application/vnd.sun.xml.calc");
            this.Add(".stc", "application/vnd.sun.xml.calc.template");
            this.Add(".sxd", "application/vnd.sun.xml.draw");
            this.Add(".std", "application/vnd.sun.xml.draw.template");
            this.Add(".sxi", "application/vnd.sun.xml.impress");
            this.Add(".sti", "application/vnd.sun.xml.impress.template");
            this.Add(".sxm", "application/vnd.sun.xml.math");
            this.Add(".sxw", "application/vnd.sun.xml.writer");
            this.Add(".sxg", "application/vnd.sun.xml.writer.global");
            this.Add(".stw", "application/vnd.sun.xml.writer.template");
            this.Add(".otf", "application/x-font-otf");
            this.Add(".osfpvg", "application/vnd.yamaha.openscoreformat.osfpvg+xml");
            this.Add(".dp", "application/vnd.osgi.dp");
            this.Add(".pdb", "application/vnd.palm");
            this.Add(".p", "text/x-pascal");
            this.Add(".paw", "application/vnd.pawaafile");
            this.Add(".pclxl", "application/vnd.hp-pclxl");
            this.Add(".efif", "application/vnd.picsel");
            this.Add(".pcx", "image/x-pcx");
            this.Add(".psd", "image/vnd.adobe.photoshop");
            this.Add(".prf", "application/pics-rules");
            this.Add(".pic", "image/x-pict");
            this.Add(".chat", "application/x-chat");
            this.Add(".p10", "application/pkcs10");
            this.Add(".p12", "application/x-pkcs12");
            this.Add(".p7m", "application/pkcs7-mime");
            this.Add(".p7s", "application/pkcs7-signature");
            this.Add(".p7r", "application/x-pkcs7-certreqresp");
            this.Add(".p7b", "application/x-pkcs7-certificates");
            this.Add(".p8", "application/pkcs8");
            this.Add(".plf", "application/vnd.pocketlearn");
            this.Add(".pnm", "image/x-portable-anymap");
            this.Add(".pbm", "image/x-portable-bitmap");
            this.Add(".pcf", "application/x-font-pcf");
            this.Add(".pfr", "application/font-tdpfr");
            this.Add(".pgn", "application/x-chess-pgn");
            this.Add(".pgm", "image/x-portable-graymap");
            this.Add(".png", "image/png");
            this.Add(".ppm", "image/x-portable-pixmap");
            this.Add(".pskcxml", "application/pskc+xml");
            this.Add(".pml", "application/vnd.ctc-posml");
            this.Add(".ai", "application/postscript");
            this.Add(".pfa", "application/x-font-type1");
            this.Add(".pbd", "application/vnd.powerbuilder6");
            this.Add("", "application/pgp-encrypted");
            this.Add(".pgp", "application/pgp-signature");
            this.Add(".box", "application/vnd.previewsystems.box");
            this.Add(".ptid", "application/vnd.pvi.ptid1");
            this.Add(".pls", "application/pls+xml");
            this.Add(".str", "application/vnd.pg.format");
            this.Add(".ei6", "application/vnd.pg.osasli");
            this.Add(".dsc", "text/prs.lines.tag");
            this.Add(".psf", "application/x-font-linux-psf");
            this.Add(".qps", "application/vnd.publishare-delta-tree");
            this.Add(".wg", "application/vnd.pmi.widget");
            this.Add(".qxd", "application/vnd.quark.quarkxpress");
            this.Add(".esf", "application/vnd.epson.esf");
            this.Add(".msf", "application/vnd.epson.msf");
            this.Add(".ssf", "application/vnd.epson.ssf");
            this.Add(".qam", "application/vnd.epson.quickanime");
            this.Add(".qfx", "application/vnd.intu.qfx");
            this.Add(".qt", "video/quicktime");
            this.Add(".rar", "application/x-rar-compressed");
            this.Add(".ram", "audio/x-pn-realaudio");
            this.Add(".rmp", "audio/x-pn-realaudio-plugin");
            this.Add(".rsd", "application/rsd+xml");
            this.Add(".rm", "application/vnd.rn-realmedia");
            this.Add(".bed", "application/vnd.realvnc.bed");
            this.Add(".mxl", "application/vnd.recordare.musicxml");
            this.Add(".musicxml", "application/vnd.recordare.musicxml+xml");
            this.Add(".rnc", "application/relax-ng-compact-syntax");
            this.Add(".rdz", "application/vnd.data-vision.rdz");
            this.Add(".rdf", "application/rdf+xml");
            this.Add(".rp9", "application/vnd.cloanto.rp9");
            this.Add(".jisp", "application/vnd.jisp");
            this.Add(".rtf", "application/rtf");
            this.Add(".rtx", "text/richtext");
            this.Add(".link66", "application/vnd.route66.link66+xml");
            this.Add(".rss, .xml", "application/rss+xml");
            this.Add(".shf", "application/shf+xml");
            this.Add(".st", "application/vnd.sailingtracker.track");
            this.Add(".svg", "image/svg+xml");
            this.Add(".sus", "application/vnd.sus-calendar");
            this.Add(".sru", "application/sru+xml");
            this.Add(".setpay", "application/set-payment-initiation");
            this.Add(".setreg", "application/set-registration-initiation");
            this.Add(".sema", "application/vnd.sema");
            this.Add(".semd", "application/vnd.semd");
            this.Add(".semf", "application/vnd.semf");
            this.Add(".see", "application/vnd.seemail");
            this.Add(".snf", "application/x-font-snf");
            this.Add(".spq", "application/scvp-vp-request");
            this.Add(".spp", "application/scvp-vp-response");
            this.Add(".scq", "application/scvp-cv-request");
            this.Add(".scs", "application/scvp-cv-response");
            this.Add(".sdp", "application/sdp");
            this.Add(".etx", "text/x-setext");
            this.Add(".movie", "video/x-sgi-movie");
            this.Add(".ifm", "application/vnd.shana.informed.formdata");
            this.Add(".itp", "application/vnd.shana.informed.formtemplate");
            this.Add(".iif", "application/vnd.shana.informed.interchange");
            this.Add(".ipk", "application/vnd.shana.informed.package");
            this.Add(".tfi", "application/thraud+xml");
            this.Add(".shar", "application/x-shar");
            this.Add(".rgb", "image/x-rgb");
            this.Add(".slt", "application/vnd.epson.salt");
            this.Add(".aso", "application/vnd.accpac.simply.aso");
            this.Add(".imp", "application/vnd.accpac.simply.imp");
            this.Add(".twd", "application/vnd.simtech-mindmapper");
            this.Add(".csp", "application/vnd.commonspace");
            this.Add(".saf", "application/vnd.yamaha.smaf-audio");
            this.Add(".mmf", "application/vnd.smaf");
            this.Add(".spf", "application/vnd.yamaha.smaf-phrase");
            this.Add(".teacher", "application/vnd.smart.teacher");
            this.Add(".svd", "application/vnd.svd");
            this.Add(".rq", "application/sparql-query");
            this.Add(".srx", "application/sparql-results+xml");
            this.Add(".gram", "application/srgs");
            this.Add(".grxml", "application/srgs+xml");
            this.Add(".ssml", "application/ssml+xml");
            this.Add(".skp", "application/vnd.koan");
            this.Add(".sgml", "text/sgml");
            this.Add(".sdc", "application/vnd.stardivision.calc");
            this.Add(".sda", "application/vnd.stardivision.draw");
            this.Add(".sdd", "application/vnd.stardivision.impress");
            this.Add(".smf", "application/vnd.stardivision.math");
            this.Add(".sdw", "application/vnd.stardivision.writer");
            this.Add(".sgl", "application/vnd.stardivision.writer-global");
            this.Add(".sm", "application/vnd.stepmania.stepchart");
            this.Add(".sit", "application/x-stuffit");
            this.Add(".sitx", "application/x-stuffitx");
            this.Add(".sdkm", "application/vnd.solent.sdkm+xml");
            this.Add(".xo", "application/vnd.olpc-sugar");
            this.Add(".au", "audio/basic");
            this.Add(".wqd", "application/vnd.wqd");
            this.Add(".sis", "application/vnd.symbian.install");
            this.Add(".smi", "application/smil+xml");
            this.Add(".xsm", "application/vnd.syncml+xml");
            this.Add(".bdm", "application/vnd.syncml.dm+wbxml");
            this.Add(".xdm", "application/vnd.syncml.dm+xml");
            this.Add(".sv4cpio", "application/x-sv4cpio");
            this.Add(".sv4crc", "application/x-sv4crc");
            this.Add(".sbml", "application/sbml+xml");
            this.Add(".tsv", "text/tab-separated-values");
            this.Add(".tiff", "image/tiff");
            this.Add(".tao", "application/vnd.tao.intent-module-archive");
            this.Add(".tar", "application/x-tar");
            this.Add(".tcl", "application/x-tcl");
            this.Add(".tex", "application/x-tex");
            this.Add(".tfm", "application/x-tex-tfm");
            this.Add(".tei", "application/tei+xml");
            this.Add(".txt", "text/plain");
            this.Add(".dxp", "application/vnd.spotfire.dxp");
            this.Add(".sfs", "application/vnd.spotfire.sfs");
            this.Add(".tsd", "application/timestamped-data");
            this.Add(".tpt", "application/vnd.trid.tpt");
            this.Add(".mxs", "application/vnd.triscape.mxs");
            this.Add(".t", "text/troff");
            this.Add(".tra", "application/vnd.trueapp");
            this.Add(".ttf", "application/x-font-ttf");
            this.Add(".ttl", "text/turtle");
            this.Add(".umj", "application/vnd.umajin");
            this.Add(".uoml", "application/vnd.uoml+xml");
            this.Add(".unityweb", "application/vnd.unity");
            this.Add(".ufd", "application/vnd.ufdl");
            this.Add(".uri", "text/uri-list");
            this.Add(".utz", "application/vnd.uiq.theme");
            this.Add(".ustar", "application/x-ustar");
            this.Add(".uu", "text/x-uuencode");
            this.Add(".vcs", "text/x-vcalendar");
            this.Add(".vcf", "text/x-vcard");
            this.Add(".vcd", "application/x-cdlink");
            this.Add(".vsf", "application/vnd.vsf");
            this.Add(".wrl", "model/vrml");
            this.Add(".vcx", "application/vnd.vcx");
            this.Add(".mts", "model/vnd.mts");
            this.Add(".vtu", "model/vnd.vtu");
            this.Add(".vis", "application/vnd.visionary");
            this.Add(".viv", "video/vnd.vivo");
            this.Add(".ccxml", "application/ccxml+xml,");
            this.Add(".vxml", "application/voicexml+xml");
            this.Add(".src", "application/x-wais-source");
            this.Add(".wbxml", "application/vnd.wap.wbxml");
            this.Add(".wbmp", "image/vnd.wap.wbmp");
            this.Add(".wav", "audio/x-wav");
            this.Add(".davmount", "application/davmount+xml");
            this.Add(".woff", "application/x-font-woff");
            this.Add(".wspolicy", "application/wspolicy+xml");
            this.Add(".webp", "image/webp");
            this.Add(".wtb", "application/vnd.webturbo");
            this.Add(".wgt", "application/widget");
            this.Add(".hlp", "application/winhlp");
            this.Add(".wml", "text/vnd.wap.wml");
            this.Add(".wmls", "text/vnd.wap.wmlscript");
            this.Add(".wmlsc", "application/vnd.wap.wmlscriptc");
            this.Add(".wpd", "application/vnd.wordperfect");
            this.Add(".stf", "application/vnd.wt.stf");
            this.Add(".wsdl", "application/wsdl+xml");
            this.Add(".xbm", "image/x-xbitmap");
            this.Add(".xpm", "image/x-xpixmap");
            this.Add(".xwd", "image/x-xwindowdump");
            this.Add(".der", "application/x-x509-ca-cert");
            this.Add(".fig", "application/x-xfig");
            this.Add(".xhtml", "application/xhtml+xml");
            this.Add(".xml", "application/xml");
            this.Add(".xdf", "application/xcap-diff+xml");
            this.Add(".xenc", "application/xenc+xml");
            this.Add(".xer", "application/patch-ops-error+xml");
            this.Add(".rl", "application/resource-lists+xml");
            this.Add(".rs", "application/rls-services+xml");
            this.Add(".rld", "application/resource-lists-diff+xml");
            this.Add(".xslt", "application/xslt+xml");
            this.Add(".xop", "application/xop+xml");
            this.Add(".xpi", "application/x-xpinstall");
            this.Add(".xspf", "application/xspf+xml");
            this.Add(".xul", "application/vnd.mozilla.xul+xml");
            this.Add(".xyz", "chemical/x-xyz");
            this.Add(".yaml", "text/yaml");
            this.Add(".yang", "application/yang");
            this.Add(".yin", "application/yin+xml");
            this.Add(".zir", "application/vnd.zul");
            this.Add(".zip", "application/zip");
            this.Add(".zmm", "application/vnd.handheld-entertainment+xml");
            this.Add(".zaz", "application/vnd.zzazz.deck+xml");
            this.Add(".sql", "text/sql");
        }

        public string GetContentType(string Extension, string AlternateExtension = "")
        {
            var h = this.Where(x => x.Key == Extension.ToLower()).Select(x => x.Value);
            if (h.Any()) return h.SingleOrDefault();
            else
            {
                if (AlternateExtension != "") return this.Where(x => x.Key == AlternateExtension.ToLower()).Select(x => x.Value).SingleOrDefault();
                else return null;
            }
        }
    }
}
