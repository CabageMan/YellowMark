using System.Data.Common;
using AutoFixture;
using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using YellowMark.AppServices.Comments.Repositories;
using YellowMark.ComponentRegistrar.MapperProfiles;
using YellowMark.Contracts.Pagination;
using YellowMark.DataAccess.Comment.Repository;
using YellowMark.DataAccess.DatabaseContext;
using YellowMark.Domain.Ads.Entity;
using YellowMark.Domain.Comments.Entity;
using YellowMark.Domain.UsersInfos.Entity;
using YellowMark.Infrastructure.Repository;

namespace YellowMark.UnitTests.Comments.Repository;

/// <summary>
/// <see cref=""/> tests.
/// </summary>
public class CommentRepositoryTests : BaseUnitTest
{
    // private ICommentRepository _commentRepository;

    // /// <summary>
    // /// Constructor for <see cref="CommentRepositoryTests"/>
    // /// </summary>
    // public CommentRepositoryTests()
    // { }

    // TODO: Try to replace SQLite by InMemoryDataBase. See API Tests.
    // Don't forget to delete SQLite dependecies.

    [Fact]
    public async Task ShouldSuccess_GetAll()
    {
        var firstComment = GenerateRandomComment();
        var secondComment = GenerateRandomComment();
        var repository = GenerateRepositoryWith(firstComment, secondComment);
        var request = new GetAllRequestWithPagination();

        var allCommentsResult = await repository.GetAllAsync(request, CancellationToken);

        Assert.NotNull(allCommentsResult);
        Assert.NotEmpty(allCommentsResult.Result);
        Assert.Equal(0, allCommentsResult.AvailablePages);
        Assert.Equal(firstComment.Id, allCommentsResult.Result.First().Id);
        Assert.Equal(secondComment.Id, allCommentsResult.Result.Last().Id);
    }

    private static CommentRepository GenerateRepositoryWith(params Comment[] comments)
    {
        IMapper mapper = new MapperConfiguration(config =>
            config.AddProfile(new CommentProfile())
        ).CreateMapper();

        var connectionForWrite = new SqliteConnection("Filename=:memory:");
        connectionForWrite.Open();

        var writeConntextOptions = new DbContextOptionsBuilder<WriteDbContext>()
            .UseSqlite(connectionForWrite)
            .Options;
        using var writeContext = new WriteDbContext(writeConntextOptions);
        writeContext.Database.EnsureDeleted();
        writeContext.Database.EnsureCreated();
        writeContext.AddRange(comments);
        writeContext.SaveChanges();

        var connectionForRead = new SqliteConnection("Filename=:memory:");
        connectionForRead.Open();

        var readConntextOptions = new DbContextOptionsBuilder<ReadDbContext>()
            .UseSqlite(connectionForRead)
            .Options;
        using var readContext = new ReadDbContext(readConntextOptions);
        readContext.Database.EnsureDeleted();
        readContext.Database.EnsureCreated();
        readContext.AddRange(comments);
        readContext.SaveChanges();

        WriteOnlyRepository<Comment, WriteDbContext> writeOnlyRepository = new(writeContext);
        ReadOnlyRepository<Comment, ReadDbContext> readOnlyRepository = new(readContext);

        return new(writeOnlyRepository, readOnlyRepository, mapper);
    }

    private static Comment GenerateRandomComment() =>
        new()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Text = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid(),
            AdId = Guid.NewGuid()
        };
}
