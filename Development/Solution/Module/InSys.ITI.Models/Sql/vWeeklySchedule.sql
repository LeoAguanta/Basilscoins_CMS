SELECT week.*, 
mon.Name Monday,
tue.name Tuesday,
wed.name Wednesday,
thu.name Thursday,
fri.name Friday,
sat.name Saturday,
sun.name Sunday FROM dbo.tWeeklySchedule week
LEFT JOIN dbo.tShiftSchedule mon ON week.ID_DailyScheduleMon = mon.ID
LEFT JOIN dbo.tShiftSchedule tue ON week.ID_DailyScheduleTue = tue.ID
LEFT JOIN dbo.tShiftSchedule wed ON week.ID_DailyScheduleWed = wed.ID
LEFT JOIN dbo.tShiftSchedule thu ON week.ID_DailyScheduleThu = thu.ID
LEFT JOIN dbo.tShiftSchedule fri ON week.ID_DailyScheduleFri = fri.ID
LEFT JOIN dbo.tShiftSchedule sat ON week.ID_DailyScheduleSat = sat.ID
LEFT JOIN dbo.tShiftSchedule sun ON week.ID_DailyScheduleSun = sun.ID