-- add military branches
INSERT INTO MilitaryBranches(Country, BranchName) VALUES ('USA', 'Air Force')
INSERT INTO MilitaryBranches(Country, BranchName) VALUES ('USA', 'Air Force Reserve')
INSERT INTO MilitaryBranches(Country, BranchName) VALUES ('USA', 'Air National Guard')
INSERT INTO MilitaryBranches(Country, BranchName) VALUES ('USA', 'Army')
INSERT INTO MilitaryBranches(Country, BranchName) VALUES ('USA', 'Army Reserve')
INSERT INTO MilitaryBranches(Country, BranchName) VALUES ('USA', 'Army National Guard')
INSERT INTO MilitaryBranches(Country, BranchName) VALUES ('USA', 'Coast Guard')
INSERT INTO MilitaryBranches(Country, BranchName) VALUES ('USA', 'Coast Guard Reserve')
INSERT INTO MilitaryBranches(Country, BranchName) VALUES ('USA', 'Marine Corps')
INSERT INTO MilitaryBranches(Country, BranchName) VALUES ('USA', 'Marine Corps Reserve')
INSERT INTO MilitaryBranches(Country, BranchName) VALUES ('USA', 'Navy')
INSERT INTO MilitaryBranches(Country, BranchName) VALUES ('USA', 'Navy Reserve')

-- add government programs
INSERT INTO dbo.GovernmentPrograms ( Name , MinAge , MaxAge , MinIncome , MaxIncome , Tier , Locality )
  VALUES  ( N'Illinois All Kids (CHIPRA)' , -- Name - nvarchar(max)
            NULL , -- MinAge - int
            19 , -- MaxAge - int
            NULL , -- MinIncome - money
            NULL , -- MaxIncome - money
            N'State' , -- Tier - nvarchar(128)
            N'USA - Illinois'  -- Locality - nvarchar(128)
          )
	INSERT INTO dbo.GovernmentPrograms ( Name , MinAge , MaxAge , MinIncome , MaxIncome , Tier , Locality )
    VALUES  ( N'Illinois Food Stamp Program' , -- Name - nvarchar(max)
              NULL , -- MinAge - int
              NULL , -- MaxAge - int
              NULL , -- MinIncome - money
              NULL , -- MaxIncome - money
              N'State' , -- Tier - nvarchar(128)
              N'USA - Illinois'  -- Locality - nvarchar(128)
            )
	INSERT INTO dbo.GovernmentPrograms ( Name , MinAge , MaxAge , MinIncome , MaxIncome , Tier , Locality )
    VALUES  ( N'Illinois Low Income Home Energy Assistance Program' , -- Name - nvarchar(max)
              NULL , -- MinAge - int
              NULL , -- MaxAge - int
              NULL , -- MinIncome - money
              NULL , -- MaxIncome - money
              N'State' , -- Tier - nvarchar(128)
              N'USA - Illinois'  -- Locality - nvarchar(128)
            )
	INSERT INTO dbo.GovernmentPrograms ( Name , MinAge , MaxAge , MinIncome , MaxIncome , Tier , Locality )
    VALUES  ( N'Illinois Medicaid' , -- Name - nvarchar(max)
              NULL , -- MinAge - int
              19 , -- MaxAge - int
              NULL , -- MinIncome - money
              NULL , -- MaxIncome - money
              N'State' , -- Tier - nvarchar(128)
              N'USA - Illinois'  -- Locality - nvarchar(128)
            )
	INSERT INTO dbo.GovernmentPrograms ( Name , MinAge , MaxAge , MinIncome , MaxIncome , Tier , Locality )
    VALUES  ( N'Illinois Special Supplemental Nutrition Program for Women, Infants, and Children (WIC)' , -- Name - nvarchar(max)
              0 , -- MinAge - int
              5 , -- MaxAge - int
              NULL , -- MinIncome - money
              NULL , -- MaxIncome - money
              N'State' , -- Tier - nvarchar(128)
              N'USA - Illinois'  -- Locality - nvarchar(128)
            )
	INSERT INTO dbo.GovernmentPrograms ( Name , MinAge , MaxAge , MinIncome , MaxIncome , Tier , Locality )
    VALUES  ( N'Illinois Temporary Assistance for Needy Families (TANF)' , -- Name - nvarchar(max)
              NULL , -- MinAge - int
              NULL , -- MaxAge - int
              NULL , -- MinIncome - money
              NULL , -- MaxIncome - money
              N'State' , -- Tier - nvarchar(128)
              N'USA - Illinois'  -- Locality - nvarchar(128)
            )
	INSERT INTO dbo.GovernmentPrograms ( Name , MinAge , MaxAge , MinIncome , MaxIncome , Tier , Locality )
    VALUES  ( N'Illinois Unemployment Insurance' , -- Name - nvarchar(max)
              NULL , -- MinAge - int
              NULL , -- MaxAge - int
              NULL , -- MinIncome - money
              NULL , -- MaxIncome - money
              N'State' , -- Tier - nvarchar(128)
              N'USA - Illinois'  -- Locality - nvarchar(128)
            )
	INSERT INTO dbo.GovernmentPrograms ( Name , MinAge , MaxAge , MinIncome , MaxIncome , Tier , Locality )
    VALUES  ( N'Illinois Weatherization Assistance Program' , -- Name - nvarchar(max)
              NULL , -- MinAge - int
              NULL , -- MaxAge - int
              NULL , -- MinIncome - money
              NULL , -- MaxIncome - money
              N'State' , -- Tier - nvarchar(128)
              N'USA - Illinois'  -- Locality - nvarchar(128)
            )
	INSERT INTO dbo.GovernmentPrograms ( Name , MinAge , MaxAge , MinIncome , MaxIncome , Tier , Locality )
    VALUES  ( N'EarnFare' , -- Name - nvarchar(max)
              NULL , -- MinAge - int
              NULL , -- MaxAge - int
              NULL , -- MinIncome - money
              NULL , -- MaxIncome - money
              N'State' , -- Tier - nvarchar(128)
              N'USA - Illinois'  -- Locality - nvarchar(128)
            )
	INSERT INTO dbo.GovernmentPrograms ( Name , MinAge , MaxAge , MinIncome , MaxIncome , Tier , Locality )
    VALUES  ( N'Government Housing' , -- Name - nvarchar(max)
              NULL , -- MinAge - int
              NULL , -- MaxAge - int
              NULL , -- MinIncome - money
              NULL , -- MaxIncome - money
              N'Municipal' , -- Tier - nvarchar(128)
              N'USA - Illinois - Cook - Chicago'  -- Locality - nvarchar(128)
            )
	INSERT INTO dbo.GovernmentPrograms ( Name , MinAge , MaxAge , MinIncome , MaxIncome , Tier , Locality )
    VALUES  ( N'LinkCard' , -- Name - nvarchar(max)
              NULL , -- MinAge - int
              NULL , -- MaxAge - int
              NULL , -- MinIncome - money
              NULL , -- MaxIncome - money
              N'Municipal' , -- Tier - nvarchar(128)
              N'USA - Illinois - Cook - Chicago'  -- Locality - nvarchar(128)
            )
	INSERT INTO dbo.GovernmentPrograms ( Name , MinAge , MaxAge , MinIncome , MaxIncome , Tier , Locality )
    VALUES  ( N'Supplemental Security Income (SSI)' , -- Name - nvarchar(max)
              NULL , -- MinAge - int
              NULL , -- MaxAge - int
              NULL , -- MinIncome - money
              NULL , -- MaxIncome - money
              N'Federal' , -- Tier - nvarchar(128)
              N'USA'  -- Locality - nvarchar(128)
            )


INSERT INTO dbo.Skills
        ( Name )
VALUES  ( N'Soft Skills'  -- Name - nvarchar(max)
          )