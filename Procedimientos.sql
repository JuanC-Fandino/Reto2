create database LibrosApp
go

use LibrosApp
go

create table libros
(
    codigo    int identity,
    titulo    varchar(40),
    autor     varchar(30),
    editorial varchar(20),
    precio    decimal(5, 2),
    cantidad  smallint,
    primary key (codigo)
);
go

create procedure listar_libros
as
select *
from libros
order by codigo
go

create procedure buscar_libros @titulo varchar(40)
as
select *
from libros
where titulo like @titulo + '%'
go

create procedure mantenimiento_libros @codigo int,
                                      @titulo varchar(40),
                                      @autor varchar(30),
                                      @editorial varchar(20),
                                      @precio decimal(5, 2),
                                      @cantidad smallint,
                                      @accion varchar(100) output
as
    if (@accion = '1')
        begin
            insert into libros(titulo, autor, editorial, precio, cantidad)
            values (@titulo, @autor, @editorial, @precio, @cantidad)
            set @accion = N'Se añadió: ' + @titulo
        end
    else
        if (@accion = '2')
            begin
                update libros
                set titulo=@titulo,
                    autor=@autor,
                    editorial=@editorial,
                    precio=@precio,
                    cantidad=@cantidad
                where codigo = @codigo
                set @accion = N'Se modifico el código: ' + cast(@codigo as varchar(100))
            end
        else
            if (@accion = '3')
                begin
                    delete from libros where codigo = @codigo
                    set @accion = N'Se borro el código: ' + cast(@codigo as varchar(100))
                end
