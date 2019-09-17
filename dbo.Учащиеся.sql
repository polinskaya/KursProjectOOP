CREATE TABLE [dbo].[Учащиеся] (
    [ID Учащегося]              INT           IDENTITY (1, 1) NOT NULL,
    [Фамилия Учащегося]         NVARCHAR (20) NOT NULL,
    [Имя Учащегося]             NVARCHAR (20) NOT NULL,
    [Отчество Учащегося]        NVARCHAR (20) NOT NULL,
    [ID группы]                 INT           NULL,
    [Дополнительная информация] XML           CONSTRAINT [DF__Учащиеся__Дополн__4E88ABD4] DEFAULT (NULL) NULL,
    [Пол]                       INT           NULL,
    CONSTRAINT [PK__Учащиеся__D41D2400C5FEA763] PRIMARY KEY CLUSTERED ([ID Учащегося] ASC),
    CONSTRAINT [FK__Учащиеся__ID гру__5EBF139D] FOREIGN KEY ([ID группы]) REFERENCES [dbo].[Группы] ([ID группы])
);

