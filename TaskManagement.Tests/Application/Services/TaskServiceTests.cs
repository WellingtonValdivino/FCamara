using FluentAssertions;
using Moq;
using TaskManagement.Application.DTOs.Request;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Services;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Tests.Application.Services;

public class TaskServiceTests
{
    private readonly Mock<ITaskRepository> _repositoryMock;
    private readonly TaskService _service;

    public TaskServiceTests()
    {
        _repositoryMock = new Mock<ITaskRepository>();
        _service = new TaskService(_repositoryMock.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateTask_WhenRequestIsValid()
    {
        var request = new CreateTaskRequest
        {
            Title = "Estudar testes unitários",
            Description = "Criar teste para o TaskService",
            DueDate = DateTime.UtcNow.AddDays(1),
            Status = TaskItemStatus.Pending
        };

        var result = await _service.CreateAsync(request);

        result.Should().NotBeNull();
        result.Id.Should().NotBe(Guid.Empty);
        result.Title.Should().Be(request.Title);
        result.Description.Should().Be(request.Description);
        result.DueDate.Should().Be(request.DueDate);
        result.Status.Should().Be(request.Status);

        _repositoryMock.Verify(
            repository => repository.AddAsync(It.IsAny<TaskItem>()),
            Times.Once);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnMappedTasks_WhenTasksExist()
    {
        var filter = new TaskFilterRequest();

        var tasks = new List<TaskItem>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Tarefa 1",
                Description = "Descrição 1",
                Status = TaskItemStatus.Pending,
                DueDate = DateTime.UtcNow.AddDays(1),
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Tarefa 2",
                Description = "Descrição 2",
                Status = TaskItemStatus.Completed,
                DueDate = DateTime.UtcNow.AddDays(2),
                CreatedAt = DateTime.UtcNow
            }
        };

        _repositoryMock
            .Setup(repository => repository.GetAllAsync(filter))
            .ReturnsAsync(tasks);

        var result = await _service.GetAllAsync(filter);

        result.Should().HaveCount(2);
        result[0].Title.Should().Be("Tarefa 1");
        result[1].Title.Should().Be("Tarefa 2");

        _repositoryMock.Verify(repository => repository.GetAllAsync(filter), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnTask_WhenTaskExists()
    {
        var id = Guid.NewGuid();

        var task = new TaskItem
        {
            Id = id,
            Title = "Tarefa encontrada",
            Description = "Descrição",
            Status = TaskItemStatus.Pending,
            DueDate = DateTime.UtcNow.AddDays(1),
            CreatedAt = DateTime.UtcNow
        };

        _repositoryMock
            .Setup(repository => repository.GetByIdAsync(id))
            .ReturnsAsync(task);

        var result = await _service.GetByIdAsync(id);

        result.Should().NotBeNull();
        result!.Id.Should().Be(id);
        result.Title.Should().Be(task.Title);
        result.Description.Should().Be(task.Description);
        result.Status.Should().Be(task.Status);

        _repositoryMock.Verify(repository => repository.GetByIdAsync(id), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenTaskDoesNotExist()
    {
        var id = Guid.NewGuid();

        _repositoryMock
            .Setup(repository => repository.GetByIdAsync(id))
            .ReturnsAsync((TaskItem?)null);

        var result = await _service.GetByIdAsync(id);

        result.Should().BeNull();

        _repositoryMock.Verify(repository => repository.GetByIdAsync(id), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateTask_WhenTaskExists()
    {
        var id = Guid.NewGuid();

        var task = new TaskItem
        {
            Id = id,
            Title = "Título antigo",
            Description = "Descrição antiga",
            Status = TaskItemStatus.Pending,
            DueDate = DateTime.UtcNow.AddDays(1),
            CreatedAt = DateTime.UtcNow
        };

        var request = new UpdateTaskRequest
        {
            Title = "Título atualizado",
            Description = "Descrição atualizada",
            Status = TaskItemStatus.InProgress,
            DueDate = DateTime.UtcNow.AddDays(3)
        };

        _repositoryMock
            .Setup(repository => repository.GetByIdAsync(id))
            .ReturnsAsync(task);

        var result = await _service.UpdateAsync(id, request);

        result.Should().BeTrue();

        task.Title.Should().Be(request.Title);
        task.Description.Should().Be(request.Description);
        task.Status.Should().Be(request.Status);
        task.DueDate.Should().Be(request.DueDate);
        task.UpdatedAt.Should().NotBeNull();

        _repositoryMock.Verify(repository => repository.UpdateAsync(task), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnFalse_WhenTaskDoesNotExist()
    {
        var id = Guid.NewGuid();

        var request = new UpdateTaskRequest
        {
            Title = "Título atualizado",
            Description = "Descrição atualizada",
            Status = TaskItemStatus.Completed,
            DueDate = DateTime.UtcNow.AddDays(2)
        };

        _repositoryMock
            .Setup(repository => repository.GetByIdAsync(id))
            .ReturnsAsync((TaskItem?)null);

        var result = await _service.UpdateAsync(id, request);

        result.Should().BeFalse();

        _repositoryMock.Verify(repository => repository.UpdateAsync(It.IsAny<TaskItem>()), Times.Never);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteTask_WhenTaskExists()
    {
        var id = Guid.NewGuid();

        var task = new TaskItem
        {
            Id = id,
            Title = "Tarefa para excluir",
            Description = "Descrição",
            Status = TaskItemStatus.Pending,
            DueDate = DateTime.UtcNow.AddDays(1),
            CreatedAt = DateTime.UtcNow
        };

        _repositoryMock
            .Setup(repository => repository.GetByIdAsync(id))
            .ReturnsAsync(task);

        var result = await _service.DeleteAsync(id);

        result.Should().BeTrue();

        _repositoryMock.Verify(repository => repository.DeleteAsync(task), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenTaskDoesNotExist()
    {
        var id = Guid.NewGuid();

        _repositoryMock
            .Setup(repository => repository.GetByIdAsync(id))
            .ReturnsAsync((TaskItem?)null);

        var result = await _service.DeleteAsync(id);

        result.Should().BeFalse();

        _repositoryMock.Verify(repository => repository.DeleteAsync(It.IsAny<TaskItem>()), Times.Never);
    }
}