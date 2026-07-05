using FluentAssertions;
using Moq;
using SegFy.API.GestaoSinistros.Application.DTO_s.Responses;
using SegFy.API.GestaoSinistros.Application.Repository;
using SegFy.API.GestaoSinistros.Application.Services;
using SegFy.API.GestaoSinistros.Domain.Sinistros;
using SegFy.API.GestaoSinistros.Domain.Sinistros.Enums;

namespace SegFy.API.GestaoSinistros.Test
{
    public class HistoricoServiceTest
    {
        [Fact]
        public async Task ConsultarHistoricoAsync_DeveRetornarNull_QuandoNaoExistirHistorico()
        {
            // Arrange
            var historicoRepo = new Mock<IHistoricoSinistroRepository>();

            historicoRepo
                .Setup(x => x.BuscaHistoricoSinistroAsync(It.IsAny<int>()))
                .ReturnsAsync((List<HistoricoSinistros>)null);

            var service = new HistoricoService(historicoRepo.Object);

            // Act
            var result = await service.ConsultarHistoricoAsync(1);

            // Assert
            result.Should().BeNull();
        }
        [Fact]
        public async Task ConsultarHistoricoAsync_DeveRetornarHistoricoMapeado_QuandoExistirDados()
        {
            // Arrange
            var historicoRepo = new Mock<IHistoricoSinistroRepository>();

            var historico = new List<HistoricoSinistros>
            {
                new HistoricoSinistros
                {
                    Id = 1,
                    StatusAnterior = StatusSinistro.Aberto,
                    StatusAtual = StatusSinistro.EmAnalise,
                    DataAtualizacao = new DateTime(2026, 01, 01)
                },
                new HistoricoSinistros
                {
                    Id = 2,
                    StatusAnterior = StatusSinistro.EmAnalise,
                    StatusAtual = StatusSinistro.Aprovado,
                    DataAtualizacao = new DateTime(2026, 01, 02)
                }
            };

            historicoRepo
                .Setup(x => x.BuscaHistoricoSinistroAsync(It.IsAny<int>()))
                .ReturnsAsync(historico);

            var service = new HistoricoService(historicoRepo.Object);

            // Act
            var result = await service.ConsultarHistoricoAsync(1);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);

            result.First().Should().BeEquivalentTo(new HistoricoSinistroResponse
            {
                Id = 1,
                StatusAnterior = StatusSinistro.Aberto,
                StatusAtual = StatusSinistro.EmAnalise,
                DataAtualizacao = new DateTime(2026, 01, 01)
            });
        }
    }
}
