using FluentAssertions;
using Moq;
using SegFy.API.GestaoSinistros.Application.DTO_s.Requests;
using SegFy.API.GestaoSinistros.Application.Exceptions;
using SegFy.API.GestaoSinistros.Application.Repository;
using SegFy.API.GestaoSinistros.Application.Services;
using SegFy.API.GestaoSinistros.Domain.Apolices;
using SegFy.API.GestaoSinistros.Domain.Apolices.Enums;
using SegFy.API.GestaoSinistros.Domain.Sinistros;
using SegFy.API.GestaoSinistros.Domain.Sinistros.Enums;

namespace SegFy.API.GestaoSinistros.Test
{
    public class SinistroServiceTest
    {

        [Fact]
        public async Task AbrirSinistroAsync_DeveLancarNotFoundException_QuandoApoliceNaoExiste()
        {
            var apoliceRepo = new Mock<IApoliceRepository>();
            var sinistroRepo = new Mock<ISinistroRepository>();

            apoliceRepo
                .Setup(x => x.BuscarApoliceNumero(It.IsAny<string>()))
                .ReturnsAsync((Apolice)null);

            var service = new SinistroService(apoliceRepo.Object, sinistroRepo.Object);

            var request = new AbrirSinistroRequest
            {
                NumeroApolice = "123",
                Descricao = "teste",
                ValorEstimado = 100
            };

            var act = async () => await service.AbrirSinistroAsync(request);

            await act.Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage("Apólice não encontrada.");
        }

        [Fact]
        public async Task AbrirSinistroAsync_DeveLancarBusinessException_QuandoApoliceNaoPermiteSinistro()
        {
            var apoliceRepo = new Mock<IApoliceRepository>();
            var sinistroRepo = new Mock<ISinistroRepository>();

            var apolice = new Apolice
            {
                Id = 1,
                Numero = "123",
                Status = StatusApolice.Cancelada
            };

            apoliceRepo
                .Setup(x => x.BuscarApoliceNumero(It.IsAny<string>()))
                .ReturnsAsync(apolice);

            var service = new SinistroService(apoliceRepo.Object, sinistroRepo.Object);

            var request = new AbrirSinistroRequest
            {
                NumeroApolice = "123",
                Descricao = "teste",
                ValorEstimado = 100
            };

            var act = async () => await service.AbrirSinistroAsync(request);

            await act.Should()
                .ThrowAsync<BusinessException>()
                .WithMessage("Não é possível abrir um sinistro para uma apólice que não está ativa.");
        }
        [Fact]
        public async Task AbrirSinistroAsync_DeveCriarSinistro_QuandoDadosValidos()
        {
            // Arrange
            var apoliceRepo = new Mock<IApoliceRepository>();
            var sinistroRepo = new Mock<ISinistroRepository>();

            var apolice = new Apolice
            {
                Id = 1,
                Numero = "123",
                Status = StatusApolice.Ativa,
                VigenciaInicio = DateTime.UtcNow.AddMonths(-1),
                VigenciaFim = DateTime.UtcNow.AddMonths(1)
            };

            apoliceRepo
                .Setup(x => x.BuscarApoliceNumero(It.IsAny<string>()))
                .ReturnsAsync(apolice);

            sinistroRepo
                .Setup(x => x.CriarSinistroAsync(It.IsAny<Sinistro>()))
                .Returns(Task.CompletedTask);

            var service = new SinistroService(apoliceRepo.Object, sinistroRepo.Object);

            var request = new AbrirSinistroRequest
            {
                NumeroApolice = "123",
                Descricao = "teste sinistro",
                ValorEstimado = 1000
            };

            // Act
            await service.AbrirSinistroAsync(request);

            // Assert
            sinistroRepo.Verify(x =>
                x.CriarSinistroAsync(It.Is<Sinistro>(s =>
                    s.ApoliceId == 1 &&
                    s.Descricao == "teste sinistro" &&
                    s.ValorEstimado == 1000
                )),
                Times.Once);
        }

        [Fact]
        public async Task AtualizarStatus_DeveLancarNotFound_QuandoSinistroNaoExiste()
        {
            var apoliceRepo = new Mock<IApoliceRepository>();
            var sinistroRepo = new Mock<ISinistroRepository>();

            sinistroRepo
                .Setup(x => x.BuscaSinistroPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Sinistro)null);

            var service = new SinistroService(apoliceRepo.Object, sinistroRepo.Object);

            var act = async () => await service.AtualizarStatusSinistroAsync(1,
                new AtualizarStatusSinistroRequest { StatusSinsitro = StatusSinistro.Aberto });

            await act.Should()
                .ThrowAsync<NotFoundException>();
        }
        [Fact]
        public async Task AtualizarStatus_DeveLancarBusinessException_QuandoStatusInvalido()
        {
            var apoliceRepo = new Mock<IApoliceRepository>();
            var sinistroRepo = new Mock<ISinistroRepository>();

            var sinistro = new Sinistro(
                apoliceId: 1,
                descricao: "teste",
                valorEstimado: 100,
                status: StatusSinistro.Aberto,
                dataAbertura: DateTime.UtcNow
            );

            sinistroRepo
                .Setup(x => x.BuscaSinistroPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(sinistro);

            var service = new SinistroService(apoliceRepo.Object, sinistroRepo.Object);

            var request = new AtualizarStatusSinistroRequest
            {
                StatusSinsitro = StatusSinistro.Encerrado,
                Motivo = null
            };

            var act = async () =>
                await service.AtualizarStatusSinistroAsync(1, request);

            await act.Should()
                .ThrowAsync<BusinessException>()
                .WithMessage("Status incorreto para o sinistro");
        }
        [Fact]
        public async Task AtualizarStatus_DeveLancarException_QuandoNegadoSemMotivo()
        {
            var apoliceRepo = new Mock<IApoliceRepository>();
            var sinistroRepo = new Mock<ISinistroRepository>();

            var sinistro = new Sinistro(1, "desc", 100, StatusSinistro.Aberto, DateTime.UtcNow);

            sinistroRepo
                .Setup(x => x.BuscaSinistroPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(sinistro);

            var service = new SinistroService(apoliceRepo.Object, sinistroRepo.Object);

            var act = async () => await service.AtualizarStatusSinistroAsync(1,
                new AtualizarStatusSinistroRequest
                {
                    StatusSinsitro = StatusSinistro.Negado,
                    Motivo = ""
                });

            await act.Should()
                .ThrowAsync<BusinessException>();
        }
        [Fact]
        public async Task AtualizarStatus_DeveAtualizarSinistro_QuandoDadosValidos()
        {
            var apoliceRepo = new Mock<IApoliceRepository>();
            var sinistroRepo = new Mock<ISinistroRepository>();

            var sinistro = new Sinistro(
                apoliceId: 1,
                descricao: "teste",
                valorEstimado: 100,
                status: StatusSinistro.Aberto,
                dataAbertura: DateTime.UtcNow
            );

            sinistroRepo
                .Setup(x => x.BuscaSinistroPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(sinistro);

            sinistroRepo
                .Setup(x => x.AtualizaSinistroAsync(It.IsAny<Sinistro>()))
                .Returns(Task.CompletedTask);

            var service = new SinistroService(apoliceRepo.Object, sinistroRepo.Object);

            var request = new AtualizarStatusSinistroRequest
            {
                StatusSinsitro = StatusSinistro.EmAnalise,
                Motivo = null
            };

            await service.AtualizarStatusSinistroAsync(1, request);

            sinistroRepo.Verify(x =>
                x.AtualizaSinistroAsync(It.Is<Sinistro>(s =>
                    s.Status == StatusSinistro.EmAnalise
                )),
                Times.Once);
        }
        [Fact]
        public async Task BuscarPorId_DeveRetornarNull_QuandoNaoExiste()
        {
            var apoliceRepo = new Mock<IApoliceRepository>();
            var sinistroRepo = new Mock<ISinistroRepository>();

            sinistroRepo
                .Setup(x => x.BuscaSinistroPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Sinistro)null);

            var service = new SinistroService(apoliceRepo.Object, sinistroRepo.Object);

            var result = await service.BuscarSinistroPorIdAsync(1);

            result.Should().BeNull();
        }
        [Fact]
        public async Task BuscarSinistroPorIdAsync_DeveRetornarSinistroResponse_QuandoEncontrado()
        {
            // Arrange
            var apoliceRepo = new Mock<IApoliceRepository>();
            var sinistroRepo = new Mock<ISinistroRepository>();

            var apolice = new Apolice
            {
                Id = 1,
                Numero = "AP123",
                Status = StatusApolice.Ativa
            };

            var sinistro = new Sinistro(
                apoliceId: 1,
                descricao: "Roubo de veículo",
                valorEstimado: 5000,
                status: StatusSinistro.Aberto,
                dataAbertura: DateTime.UtcNow
            );

            // importante: simular navegação EF (sinistro.Apolice)
            sinistro.Apolice = apolice;

            sinistroRepo
                .Setup(x => x.BuscaSinistroPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(sinistro);

            var service = new SinistroService(apoliceRepo.Object, sinistroRepo.Object);

            // Act
            var result = await service.BuscarSinistroPorIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(sinistro.Id);
            result.Descricao.Should().Be("Roubo de veículo");
            result.ValorEstimado.Should().Be(5000);
            result.Status.Should().Be(StatusSinistro.Aberto);

            result.Apolice.Should().NotBeNull();
            result.Apolice.Id.Should().Be(1);
            result.Apolice.Numero.Should().Be("AP123");
            result.Apolice.Status.Should().Be(StatusApolice.Ativa);
        }
        
    }
}
