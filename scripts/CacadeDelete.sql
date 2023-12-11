-- Alter foreign key constraint to enable cascade delete
ALTER TABLE [dbo].[OrderLine]
ADD CONSTRAINT [FK_OrderLine_OrderHeader]
FOREIGN KEY ([OrderHeaderId])
REFERENCES [dbo].[OrderHeader] ([Id])
ON DELETE CASCADE;