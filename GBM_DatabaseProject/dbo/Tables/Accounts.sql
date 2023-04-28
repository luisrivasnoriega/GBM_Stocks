CREATE TABLE [dbo].[Accounts] (
    [AccountId] INT   IDENTITY (1, 1) NOT NULL,
    [Cash]      MONEY NULL,
    CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED ([AccountId] ASC)
);

