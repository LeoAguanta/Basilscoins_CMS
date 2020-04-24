SELECT tpa.*, tp.Name Province, tc.Name City, tb.Name Barangay FROM dbo.tPersonaAddress tpa
LEFT JOIN dbo.tProvince tp ON tpa.ID_Province = tp.ID
LEFT JOIN dbo.tCity tc ON tpa.ID_City = tc.ID
LEFT JOIN dbo.tBarangay tb ON tpa.ID_Barangay = tb.ID