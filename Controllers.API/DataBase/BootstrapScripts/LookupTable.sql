IF NOT EXISTS (SELECT id FROM [LookupTable] WHERE Name = 'tblCamps')
BEGIN
INSERT INTO [dbo].[LookupTable]
           ([Name]
           ,[Description])
     VALUES
           ('tblCamps','lookup table for tblCamps')
END

IF NOT EXISTS (SELECT id FROM [LookupTable] WHERE Name = 'InvoiceType')
BEGIN
INSERT INTO [dbo].[LookupTable]
           ([Name]
           ,[Description])
     VALUES
           ('InvoiceType','InvoiceType')
END


IF NOT EXISTS (SELECT id FROM [LookupTable] WHERE Name = 'Format')
BEGIN
INSERT INTO [dbo].[LookupTable]
           ([Name]
           ,[Description])
     VALUES
           ('Format','Format')
END

IF NOT EXISTS (SELECT id FROM [LookupTable] WHERE Name = '	MealPlan')
BEGIN
INSERT INTO [dbo].[LookupTable]
           ([Name]
           ,[Description])
     VALUES
           ('MealPlan','MealPlan')
END



IF NOT EXISTS (SELECT id FROM [LookupTable] WHERE Name = 'AddinType')
BEGIN
INSERT INTO [dbo].[LookupTable]
           ([Name]
           ,[Description])
     VALUES
           ('AddinType','AddinType')
END

IF NOT EXISTS (SELECT id FROM [LookupTable] WHERE Name = 'StudentStatus')
BEGIN
INSERT INTO [dbo].[LookupTable]
           ([Name]
           ,[Description])
     VALUES
           ('StudentStatus','StudentStatus')
END

IF NOT EXISTS (SELECT id FROM [LookupTable] WHERE Name = 'ChapFamily')
BEGIN
INSERT INTO [dbo].[LookupTable]
           ([Name]
           ,[Description])
     VALUES
           ('ChapFamily','ChapFamily')
END