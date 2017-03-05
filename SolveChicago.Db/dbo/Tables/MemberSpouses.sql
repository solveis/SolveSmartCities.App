CREATE TABLE [MemberSpouses]
(
    [Spouse_1_Id] INT NOT NULL, 
    [Spouse_2_Id] INT NOT NULL, 
    [IsCurrent] BIT NOT NULL DEFAULT 1, 

    PRIMARY KEY ([Spouse_1_Id], [Spouse_2_Id]), 
    CONSTRAINT [FK_MemberSpouses_Spouse1] FOREIGN KEY ([Spouse_1_Id]) REFERENCES [Members]([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_MemberSpouses_Spouse2] FOREIGN KEY ([Spouse_2_Id]) REFERENCES [Members]([Id]) ON DELETE NO ACTION,
)
