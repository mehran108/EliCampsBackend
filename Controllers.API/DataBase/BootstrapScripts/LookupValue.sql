IF NOT EXISTS (SELECT id FROM LookupValue WHERE Name = 'EliAcademy' 
and lookupTableId = (Select top 1 id from LookupTable  where name = 'tblCamps'))
BEGIN
INSERT INTO [dbo].[LookupValue]
           ([Name]
           ,[Description]
		   ,lookupTableId)
     VALUES
           ('EliAcademy', 'EliAcademy', (Select top 1 id from LookupTable  where name = 'tblCamps'))
END



IF NOT EXISTS (SELECT id FROM LookupValue WHERE Name = 'EliCAMPS' 
and lookupTableId = (Select top 1 id from LookupTable  where name = 'tblCamps'))
BEGIN
INSERT INTO [dbo].[LookupValue]
           ([Name]
           ,[Description]
		   ,lookupTableId)
     VALUES
           ('EliCAMPS', 'EliCAMPS', (Select top 1 id from LookupTable  where name = 'tblCamps'))
END

IF NOT EXISTS (SELECT id FROM LookupValue WHERE Name = 'Gross' 
and lookupTableId = (Select top 1 id from LookupTable  where name = 'InvoiceType'))
BEGIN
INSERT INTO [dbo].[LookupValue]
           ([Name]
           ,[Description]
		   ,lookupTableId)
     VALUES
           ('Gross', 'Gross', (Select top 1 id from LookupTable  where name = 'InvoiceType'))
END

IF NOT EXISTS (SELECT id FROM LookupValue WHERE Name = 'Net' 
and lookupTableId = (Select top 1 id from LookupTable  where name = 'InvoiceType'))
BEGIN
INSERT INTO [dbo].[LookupValue]
           ([Name]
           ,[Description]
		   ,lookupTableId)
     VALUES
           ('Net', 'Net', (Select top 1 id from LookupTable  where name = 'InvoiceType'))
END