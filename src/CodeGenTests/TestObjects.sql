if schema_id('sqlexeccodegentests') is null begin
    exec ('create schema sqlexeccodegentests')
end;

GO

create or alter procedure sqlexeccodegentests.ReturnThreeNoPar
as begin
    return 3
end;

GO
