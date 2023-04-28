CREATE TABLE [dbo].[ShareByAccounts] (
    [ShareByAccountId] INT         IDENTITY (1, 1) NOT NULL,
    [AccountId]        INT         NOT NULL,
    [IssuerName]       VARCHAR (5) NOT NULL,
    [SharePrice]       MONEY       NOT NULL,
    [TotalShare]       INT         NOT NULL,
    CONSTRAINT [PK_SharesByAccounts] PRIMARY KEY CLUSTERED ([ShareByAccountId] ASC),
    CONSTRAINT [FK_SharesByAccounts_Accounts] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Accounts] ([AccountId])
);

