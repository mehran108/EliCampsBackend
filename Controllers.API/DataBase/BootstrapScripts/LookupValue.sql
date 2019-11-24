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

IF NOT EXISTS (SELECT id FROM LookupValue WHERE Name = 'Day Program' 
and lookupTableId = (Select top 1 id from LookupTable  where name = 'Format'))
BEGIN
INSERT INTO [dbo].[LookupValue]
           ([Name]
           ,[Description]
		   ,lookupTableId)
     VALUES
           ('Day Program', 'Day Program', (Select top 1 id from LookupTable  where name = 'Format'))
END



IF NOT EXISTS (SELECT id FROM LookupValue WHERE Name = 'Homestay' 
and lookupTableId = (Select top 1 id from LookupTable  where name = 'Format'))
BEGIN
INSERT INTO [dbo].[LookupValue]
           ([Name]
           ,[Description]
		   ,lookupTableId)
     VALUES
           ('Homestay', 'Homestay', (Select top 1 id from LookupTable  where name = 'Format'))
END


IF NOT EXISTS (SELECT id FROM LookupValue WHERE Name = 'Live-in Program' 
and lookupTableId = (Select top 1 id from LookupTable  where name = 'Format'))
BEGIN
INSERT INTO [dbo].[LookupValue]
           ([Name]
           ,[Description]
		   ,lookupTableId)
     VALUES
           ('Live-in Program', 'Live-in Program', (Select top 1 id from LookupTable  where name = 'Format'))
END


IF NOT EXISTS (SELECT id FROM LookupValue WHERE Name = 'School Program' 
and lookupTableId = (Select top 1 id from LookupTable  where name = 'Format'))
BEGIN
INSERT INTO [dbo].[LookupValue]
           ([Name]
           ,[Description]
		   ,lookupTableId)
     VALUES
           ('School Program', 'School Program', (Select top 1 id from LookupTable  where name = 'Format'))
END


IF NOT EXISTS (SELECT id FROM LookupValue WHERE Name = 'Full Board' 
and lookupTableId = (Select top 1 id from LookupTable  where name = 'MealPlan'))
BEGIN
INSERT INTO [dbo].[LookupValue]
           ([Name]
           ,[Description]
		   ,lookupTableId)
     VALUES
           ('Full Board', 'Full Board', (Select top 1 id from LookupTable  where name = 'MealPlan'))
END

IF NOT EXISTS (SELECT id FROM LookupValue WHERE Name = 'Half Board' 
and lookupTableId = (Select top 1 id from LookupTable  where name = 'MealPlan'))
BEGIN
INSERT INTO [dbo].[LookupValue]
           ([Name]
           ,[Description]
		   ,lookupTableId)
     VALUES
           ('Half Board', 'Half Board', (Select top 1 id from LookupTable  where name = 'MealPlan'))
END

IF NOT EXISTS (SELECT id FROM LookupValue WHERE Name = 'Breakfast Only' 
and lookupTableId = (Select top 1 id from LookupTable  where name = 'MealPlan'))
BEGIN
INSERT INTO [dbo].[LookupValue]
           ([Name]
           ,[Description]
		   ,lookupTableId)
     VALUES
           ('Breakfast Only', 'Breakfast Only', (Select top 1 id from LookupTable  where name = 'MealPlan'))
END

IF NOT EXISTS (SELECT id FROM LookupValue WHERE Name = 'No Meals' 
and lookupTableId = (Select top 1 id from LookupTable  where name = 'MealPlan'))
BEGIN
INSERT INTO [dbo].[LookupValue]
           ([Name]
           ,[Description]
		   ,lookupTableId)
     VALUES
           ('No Meals', 'No Meals', (Select top 1 id from LookupTable  where name = 'MealPlan'))
END


IF NOT EXISTS (SELECT id FROM LookupValue WHERE Name = 'Lunch Only' 
and lookupTableId = (Select top 1 id from LookupTable  where name = 'MealPlan'))
BEGIN
INSERT INTO [dbo].[LookupValue]
           ([Name]
           ,[Description]
		   ,lookupTableId)
     VALUES
           ('Lunch Only', 'Lunch Only', (Select top 1 id from LookupTable  where name = 'MealPlan'))
END



IF NOT EXISTS (SELECT id FROM LookupValue WHERE Name = 'Additional services' 
and lookupTableId = (Select top 1 id from LookupTable  where name = 'AddinType'))
BEGIN
INSERT INTO [dbo].[LookupValue]
           ([Name]
           ,[Description]
		   ,lookupTableId)
     VALUES
           ('Additional services', 'Additional services', (Select top 1 id from LookupTable  where name = 'AddinType'))
END



IF NOT EXISTS (SELECT id FROM LookupValue WHERE Name = 'Included services' 
and lookupTableId = (Select top 1 id from LookupTable  where name = 'AddinType'))
BEGIN
INSERT INTO [dbo].[LookupValue]
           ([Name]
           ,[Description]
		   ,lookupTableId)
     VALUES
           ('Included services', 'Included services', (Select top 1 id from LookupTable  where name = 'AddinType'))
END

IF NOT EXISTS (SELECT id FROM LookupValue WHERE Name = 'Active' 
and lookupTableId = (Select top 1 id from LookupTable  where name = 'StudentStatus'))
BEGIN
INSERT INTO [dbo].[LookupValue]
           ([Name]
           ,[Description]
		   ,lookupTableId)
     VALUES
           ('Active', 'Active', (Select top 1 id from LookupTable  where name = 'StudentStatus'))
END


IF NOT EXISTS (SELECT id FROM LookupValue WHERE Name = 'Canceled' 
and lookupTableId = (Select top 1 id from LookupTable  where name = 'StudentStatus'))
BEGIN
INSERT INTO [dbo].[LookupValue]
           ([Name]
           ,[Description]
		   ,lookupTableId)
     VALUES
           ('Canceled', 'Canceled', (Select top 1 id from LookupTable  where name = 'StudentStatus'))
END