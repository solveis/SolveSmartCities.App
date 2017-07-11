-- rename
sp_rename 'NonprofitMembersCaseManagers', 'NonprofitMembersStaff'

-- schema compare, adds first
-- schema compare the rest, skip MemberSkills and NonprofitMemberStaff, NonprofitMembers
-- insert into nonprofitstaff for each connection

INSERT INTO dbo.NonprofitStaff
        ( NonprofitId ,
          CaseManagerId ,
          ProgramId ,
          Role
        )
VALUES (  1 , -- NonprofitId - int
          3 , -- CaseManagerId - int
          NULL , -- ProgramId - int
          N'CaseManager'  -- Role - nvarchar(50)
        )
INSERT INTO dbo.NonprofitStaff
        ( NonprofitId ,
          CaseManagerId ,
          ProgramId ,
          Role
        )
VALUES (  2 , -- NonprofitId - int
          3 , -- CaseManagerId - int
          NULL , -- ProgramId - int
          N'CaseManager'  -- Role - nvarchar(50)
        )
INSERT INTO dbo.NonprofitStaff
        ( NonprofitId ,
          CaseManagerId ,
          ProgramId ,
          Role
        )
VALUES (  3 , -- NonprofitId - int
          7 , -- CaseManagerId - int
          NULL , -- ProgramId - int
          N'CaseManager'  -- Role - nvarchar(50)
        )
INSERT INTO dbo.NonprofitStaff
        ( NonprofitId ,
          CaseManagerId ,
          ProgramId ,
          Role
        )
VALUES (  4 , -- NonprofitId - int
          7 , -- CaseManagerId - int
          NULL , -- ProgramId - int
          N'CaseManager'  -- Role - nvarchar(50)
        )
INSERT INTO dbo.NonprofitStaff
        ( NonprofitId ,
          CaseManagerId ,
          ProgramId ,
          Role
        )
VALUES (  5 , -- NonprofitId - int
          7 , -- CaseManagerId - int
          NULL , -- ProgramId - int
          N'CaseManager'  -- Role - nvarchar(50)
        )
INSERT INTO dbo.NonprofitStaff
        ( NonprofitId ,
          CaseManagerId ,
          ProgramId ,
          Role
        )
VALUES (  6 , -- NonprofitId - int
          8 , -- CaseManagerId - int
          NULL , -- ProgramId - int
          N'CaseManager'  -- Role - nvarchar(50)
        )
INSERT INTO dbo.NonprofitStaff
        ( NonprofitId ,
          CaseManagerId ,
          ProgramId ,
          Role
        )
VALUES (  7 , -- NonprofitId - int
          8 , -- CaseManagerId - int
          NULL , -- ProgramId - int
          N'CaseManager'  -- Role - nvarchar(50)
        )
INSERT INTO dbo.NonprofitStaff
        ( NonprofitId ,
          CaseManagerId ,
          ProgramId ,
          Role
        )
VALUES (  8 , -- NonprofitId - int
          9 , -- CaseManagerId - int
          NULL , -- ProgramId - int
          N'CaseManager'  -- Role - nvarchar(50)
        )


-- mark all NPO as having programs
UPDATE Nonprofits SET HasPrograms = 1, ProviderType = 'Workforce'

-- insert a program for a each existing NPO
INSERT INTO dbo.NonprofitPrograms
        ( NonprofitId ,
          ProgramName ,
          MinAge ,
          MaxAge ,
          Gender ,
          EthnicityId
        )
VALUES  ( 2 , -- NonprofitId - int
          N'' , -- ProgramName - nvarchar(128)
          NULL , -- MinAge - int
          NULL , -- MaxAge - int
          NULL , -- Gender - nvarchar(50)
          NULL  -- EthnicityId - int
        )
INSERT INTO dbo.NonprofitPrograms
        ( NonprofitId ,
          ProgramName ,
          MinAge ,
          MaxAge ,
          Gender ,
          EthnicityId
        )
VALUES  ( 3 , -- NonprofitId - int
          N'Manufacturing Performance Center' , -- ProgramName - nvarchar(128)
          NULL , -- MinAge - int
          NULL , -- MaxAge - int
          NULL , -- Gender - nvarchar(50)
          NULL  -- EthnicityId - int
        )
INSERT INTO dbo.NonprofitPrograms
        ( NonprofitId ,
          ProgramName ,
          MinAge ,
          MaxAge ,
          Gender ,
          EthnicityId
        )
VALUES  ( 4 , -- NonprofitId - int
          N're:work training' , -- ProgramName - nvarchar(128)
          NULL , -- MinAge - int
          NULL , -- MaxAge - int
          NULL , -- Gender - nvarchar(50)
          NULL  -- EthnicityId - int
        )
INSERT INTO dbo.NonprofitPrograms
        ( NonprofitId ,
          ProgramName ,
          MinAge ,
          MaxAge ,
          Gender ,
          EthnicityId
        )
VALUES  ( 5 , -- NonprofitId - int
          N'First Bethel' , -- ProgramName - nvarchar(128)
          NULL , -- MinAge - int
          NULL , -- MaxAge - int
          NULL , -- Gender - nvarchar(50)
          NULL  -- EthnicityId - int
        )
INSERT INTO dbo.NonprofitPrograms
        ( NonprofitId ,
          ProgramName ,
          MinAge ,
          MaxAge ,
          Gender ,
          EthnicityId
        )
VALUES  ( 6 , -- NonprofitId - int
          N'The Ideal Candidate ' , -- ProgramName - nvarchar(128)
          NULL , -- MinAge - int
          NULL , -- MaxAge - int
          NULL , -- Gender - nvarchar(50)
          NULL  -- EthnicityId - int
        )
INSERT INTO dbo.NonprofitPrograms
        ( NonprofitId ,
          ProgramName ,
          MinAge ,
          MaxAge ,
          Gender ,
          EthnicityId
        )
VALUES  ( 7 , -- NonprofitId - int
          N'Cara' , -- ProgramName - nvarchar(128)
          NULL , -- MinAge - int
          NULL , -- MaxAge - int
          NULL , -- Gender - nvarchar(50)
          NULL  -- EthnicityId - int
        )
INSERT INTO dbo.NonprofitPrograms
        ( NonprofitId ,
          ProgramName ,
          MinAge ,
          MaxAge ,
          Gender ,
          EthnicityId
        )
VALUES  ( 8 , -- NonprofitId - int
          N'BLUE1647' , -- ProgramName - nvarchar(128)
          NULL , -- MinAge - int
          NULL , -- MaxAge - int
          NULL , -- Gender - nvarchar(50)
          NULL  -- EthnicityId - int
        )
INSERT INTO dbo.NonprofitPrograms
        ( NonprofitId ,
          ProgramName ,
          MinAge ,
          MaxAge ,
          Gender ,
          EthnicityId
        )
VALUES  ( 9 , -- NonprofitId - int
          N'CodeNow' , -- ProgramName - nvarchar(128)
          NULL , -- MinAge - int
          NULL , -- MaxAge - int
          NULL , -- Gender - nvarchar(50)
          NULL  -- EthnicityId - int
        )
INSERT INTO dbo.NonprofitPrograms
        ( NonprofitId ,
          ProgramName ,
          MinAge ,
          MaxAge ,
          Gender ,
          EthnicityId
        )
VALUES  ( 10 , -- NonprofitId - int
          N'Leave No Veteran Behind' , -- ProgramName - nvarchar(128)
          NULL , -- MinAge - int
          NULL , -- MaxAge - int
          NULL , -- Gender - nvarchar(50)
          NULL  -- EthnicityId - int
        )
INSERT INTO dbo.NonprofitPrograms
        ( NonprofitId ,
          ProgramName ,
          MinAge ,
          MaxAge ,
          Gender ,
          EthnicityId
        )
VALUES  ( 11 , -- NonprofitId - int
          N'The Dovetail Project' , -- ProgramName - nvarchar(128)
          NULL , -- MinAge - int
          NULL , -- MaxAge - int
          NULL , -- Gender - nvarchar(50)
          NULL  -- EthnicityId - int
        )
INSERT INTO dbo.NonprofitPrograms
        ( NonprofitId ,
          ProgramName ,
          MinAge ,
          MaxAge ,
          Gender ,
          EthnicityId
        )
VALUES  ( 13 , -- NonprofitId - int
          N'' , -- ProgramName - nvarchar(128)
          NULL , -- MinAge - int
          NULL , -- MaxAge - int
          NULL , -- Gender - nvarchar(50)
          NULL  -- EthnicityId - int
        )



-- insert ethnicities

INSERT INTO dbo.Ethnicities
        ( EthnicityName )
VALUES  ( N'American Indian or Alaska Native'  -- EthnicityName - nvarchar(50)
          )

INSERT INTO dbo.Ethnicities
        ( EthnicityName )
VALUES  ( N'Asian'  -- EthnicityName - nvarchar(50)
          )

INSERT INTO dbo.Ethnicities
        ( EthnicityName )
VALUES  ( N'Black or African American'  -- EthnicityName - nvarchar(50)
          )

INSERT INTO dbo.Ethnicities
        ( EthnicityName )
VALUES  ( N'Hispanic or Latino'  -- EthnicityName - nvarchar(50)
          )

INSERT INTO dbo.Ethnicities
        ( EthnicityName )
VALUES  ( N'Native Hawaiian or Other Pacific Islander'  -- EthnicityName - nvarchar(50)
          )

INSERT INTO dbo.Ethnicities
        ( EthnicityName )
VALUES  ( N'White'  -- EthnicityName - nvarchar(50)
          )

-- update all soft skills
UPDATE Skills SET IsWorkforce = 0 WHERE Id = 1

-- make sure all MemberSkills reference to new FK to NonprofitSkillsId
update ms SET ms.nonprofitskillsid =  ns.id FROM memberskills ms INNER JOIN nonprofitskills ns ON ms.nonprofitskillsid = ns.nonprofitid AND ms.SkillId = ns.SkillId

-- make sure NonprofitMemberStaff references the new FK to NonprofitStaffId
UPDATE nms SET nms.nonprofitstaffid = ns.id FROM nonprofitmembersstaff nms INNER JOIN nonprofitstaff ns ON nms.nonprofitstaffid = ns.casemanagerid

-- make sure nonprofitmembers references the correct FK to nonprofitprograms
UPDATE nonprofitmembers SET programid = nonprofitid

-- make sure nonprofitskills has the correct programid
UPDATE ns SET ns.programid = np.id FROM nonprofits n INNER JOIN nonprofitprograms np ON n.id = np.nonprofitid INNER JOIN nonprofitskills ns ON ns.nonprofitid = n.id WHERE ns.programid IS NULL