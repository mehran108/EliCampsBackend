
IF NOT EXISTS (SELECT clmYear_ID FROM tblYears WHERE clmYear_Year = 2019)
BEGIN
INSERT INTO [dbo].[tblYears]
           ([clmYear_Year])
     VALUES
           (2019)
END


IF NOT EXISTS (SELECT clmYear_ID FROM tblYears WHERE clmYear_Year = 2020)
BEGIN
INSERT INTO [dbo].[tblYears]
           ([clmYear_Year])
     VALUES
           (2020)
END