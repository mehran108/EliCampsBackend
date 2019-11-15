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