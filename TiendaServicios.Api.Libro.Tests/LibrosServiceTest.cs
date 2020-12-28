using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiendaServicios.Api.Libro.Aplicacion;
using TiendaServicios.Api.Libro.Persistencia;
using Xunit;

namespace TiendaServicios.Api.Libro.Tests
{
    public class LibrosServiceTest
    {
        private IEnumerable<Modelo.Libro> ObtenerDataPrueba()
        {
            A.Configure<Modelo.Libro>()
                .Fill(x => x.Titulo).AsArticleTitle()
                .Fill(x => x.LibroId, () => { return Guid.NewGuid(); });

            var lista = A.ListOf<Modelo.Libro>(30);
            lista[0].LibroId = Guid.Empty;
            return lista;
        }

        private Mock<ContextoLibro> CrearContexto()
        {
            var dataPrueba = ObtenerDataPrueba().AsQueryable();
            var dbSet = new Mock<DbSet<Modelo.Libro>>();
            dbSet.As<IQueryable<Modelo.Libro>>().Setup(x => x.Provider).Returns(dataPrueba.Provider);
            dbSet.As<IQueryable<Modelo.Libro>>().Setup(x => x.Expression).Returns(dataPrueba.Expression);
            dbSet.As<IQueryable<Modelo.Libro>>().Setup(x => x.ElementType).Returns(dataPrueba.ElementType);
            dbSet.As<IQueryable<Modelo.Libro>>().Setup(x => x.GetEnumerator()).Returns(dataPrueba.GetEnumerator());

            dbSet.As<IAsyncEnumerable<Modelo.Libro>>().Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsyncEnumerator<Modelo.Libro>(dataPrueba.GetEnumerator()));

            dbSet.As<IQueryable<Modelo.Libro>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<Modelo.Libro>(dataPrueba.Provider));
            
            var context = new Mock<ContextoLibro>();
            context.Setup(x => x.Libro).Returns(dbSet.Object);

            return context;
        }

        [Fact]
        public async void GetLibroPorId()
        {
            var mockContexto = CrearContexto();
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });

            var mockMapper = mapConfig.CreateMapper();

            var request = new ConsultaFiltro.LibroUnico();
            request.LibroGuid = Guid.Empty;

            ConsultaFiltro.Manejador manejador = new ConsultaFiltro.Manejador(mockContexto.Object, mockMapper);
            var libro = await manejador.Handle(request, new System.Threading.CancellationToken());
            Assert.NotNull(libro);
            Assert.True(libro.LibroId == Guid.Empty);
            
        }

        [Fact]
        public async void GetLibros()
        {
            System.Diagnostics.Debugger.Launch();
            //var mockContexto = new Mock<ContextoLibro>();
            //var mockMapper = new Mock<IMapper>();

            // Consulta.Manejador manejador = new Consulta.Manejador(mockContexto.Object, mockMapper.Object);

            var mockContexto = CrearContexto();
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });

            var mockMapper = mapConfig.CreateMapper();

            Consulta.Manejador manejador = new Consulta.Manejador(mockContexto.Object, mockMapper);

            Consulta.Ejecuta request = new Consulta.Ejecuta();

            var lista = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.True(lista.Any());
        }

        [Fact]
        public async void GuardarLibro()
        {
            var options = new DbContextOptionsBuilder<ContextoLibro>()
                .UseInMemoryDatabase(databaseName: "LibroTest")
                .Options;

            var contexto = new ContextoLibro(options);
            var request = new Nuevo.Ejecuta();

            request.Titulo = "Libro de Testing";
            request.AutorLibro = Guid.Empty;
            request.FechaPublicacion = DateTime.Now;

            var manejador = new Nuevo.Manejador(contexto);

            var resultado = await manejador.Handle(request, new System.Threading.CancellationToken());
            Assert.True(resultado != null);
        }
    }
}
