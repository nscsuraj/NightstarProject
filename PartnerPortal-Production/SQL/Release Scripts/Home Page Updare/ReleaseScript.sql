SET IDENTITY_INSERT PageInfo ON
/*
INSERT INTO PageInfo(Id,PageType,Title,TitleTag,[Description],CMSJson,[Status],CreateDate,LastUpdated,LayoutType,IsTemplate,IsCustomTemplate,Sites)
SELECT 2,PageType,Title,TitleTag,[Description],CMSJson,[Status],CreateDate,LastUpdated,LayoutType,IsTemplate,IsCustomTemplate,Sites
FROM PageInfo WHERE Id = 1

INSERT INTO PageInfo(Id,PageType,Title,TitleTag,[Description],CMSJson,[Status],CreateDate,LastUpdated,LayoutType,IsTemplate,IsCustomTemplate,Sites)
SELECT 3,PageType,Title,TitleTag,[Description],CMSJson,[Status],CreateDate,LastUpdated,LayoutType,IsTemplate,IsCustomTemplate,Sites
FROM PageInfo WHERE Id = 1

INSERT INTO PageInfo(Id,PageType,Title,TitleTag,[Description],CMSJson,[Status],CreateDate,LastUpdated,LayoutType,IsTemplate,IsCustomTemplate,Sites)
SELECT 4,PageType,Title,TitleTag,[Description],CMSJson,[Status],CreateDate,LastUpdated,LayoutType,IsTemplate,IsCustomTemplate,Sites
FROM PageInfo WHERE Id = 1

INSERT INTO PageInfo(Id,PageType,Title,TitleTag,[Description],CMSJson,[Status],CreateDate,LastUpdated,LayoutType,IsTemplate,IsCustomTemplate,Sites)
SELECT 5,PageType,Title,TitleTag,[Description],CMSJson,[Status],CreateDate,LastUpdated,LayoutType,IsTemplate,IsCustomTemplate,Sites
FROM PageInfo WHERE Id = 1

INSERT INTO PageInfo(Id,PageType,Title,TitleTag,[Description],CMSJson,[Status],CreateDate,LastUpdated,LayoutType,IsTemplate,IsCustomTemplate,Sites)
SELECT 6,PageType,Title,TitleTag,[Description],CMSJson,[Status],CreateDate,LastUpdated,LayoutType,IsTemplate,IsCustomTemplate,Sites
FROM PageInfo WHERE Id = 1*/

INSERT INTO PageInfo(Id,PageType,Title,TitleTag,[Description],CMSJson,[Status],CreateDate,LastUpdated,LayoutType,IsTemplate,IsCustomTemplate,Sites)
SELECT 7,PageType,Title,TitleTag,[Description],CMSJson,[Status],CreateDate,LastUpdated,LayoutType,IsTemplate,IsCustomTemplate,Sites
FROM PageInfo WHERE Id = 1

SET IDENTITY_INSERT PageInfo OFF


--not run at development already done. run only in production
--INSERT INTO CMS_ElementProperty (ElementId, ElementProperty) VALUES(52,'Button Hover Text Color') --Generate ID = 711
--INSERT INTO CMS_ElementProperty (ElementId, ElementProperty) VALUES(52,'Button Hover Background Color') --Generate ID = 712
