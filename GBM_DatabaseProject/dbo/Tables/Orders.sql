CREATE TABLE [dbo].[Orders] (
    [OrderId]    INT         IDENTITY (1, 1) NOT NULL,
    [IssuerName] VARCHAR (5) NOT NULL,
    [Shares]     INT         NOT NULL,
    [SharePrice] MONEY       NOT NULL,
    [Timestamp]  INT         NOT NULL,
    [AccountId]  INT         NOT NULL,
    [Operation]  BIT         NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([OrderId] ASC),
    CONSTRAINT [FK_Orders_Accounts] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Accounts] ([AccountId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1 Buy, 0 Sell', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Orders', @level2type = N'COLUMN', @level2name = N'Operation';

