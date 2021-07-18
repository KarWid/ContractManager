namespace ContractManager.Common.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Moq;
    using MongoDB.Driver;
    using NUnit.Framework;
    using ContractManager.Common.Services;
    using ContractManager.Repository.Repositories;
    using ContractManager.Repository.Entities;
    using ContractManager.Common.Models;
    
    public class ParagraphServiceTests
    {
        private ParagraphService _paragraphService;
        private readonly Mock<IRepository<ParagraphEntity>> _paragraphRepositoryMock = new Mock<IRepository<ParagraphEntity>>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private List<ParagraphEntity> _existingParagraphs;

        [SetUp]
        public void Setup()
        {
            SetupExistingParagraphs();
            SetupParagraphRepository();
            SetupMapper();

            _paragraphService = new ParagraphService(_paragraphRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task ParagraphServiceShouldRemoveExistingItem()
        {
            // Arrange
            var idToDelete = _existingParagraphs.First().Id;

            // Act
            var result = await _paragraphService.DeleteAsync(idToDelete);

            // Assert
            Assert.That(_existingParagraphs.FirstOrDefault(_ => _.Id == idToDelete) == null, "Item should be removed!");
        }

        [Test]
        public async Task ParagraphServiceShouldAddNewItem()
        {
            // Arrange
            var newId = "12312";
            var newParagraph = new Paragraph { Id = newId, Content = "New unique entity content" };

            // Act
            var addedParagraph = await _paragraphService.AddAsync(newParagraph);

            // Assert
            // TODO @KWidla: change it to check if Name is created in existing paragraphs
            Assert.That(_existingParagraphs.Any(_ => _.Content == newParagraph.Content), "Item should be added!");
            Assert.That(addedParagraph.Id != newId, "Id for new item should be generated!");
        }

        [Test]
        public async Task ParagraphServiceShouldEditExistingItem()
        {
            // Arrange
            var newContent = "Changed content";

            var firstParagraph = _existingParagraphs.FirstOrDefault();
            var paragraphToUpdate = new Paragraph { Id = firstParagraph.Id, Content = newContent };

            // Act
            var result = await _paragraphService.EditAsync(paragraphToUpdate);

            // Assert
            Assert.That(
                _existingParagraphs.FirstOrDefault(_ => _.Id == firstParagraph.Id).Content == newContent, 
                "Content should be updated!");
        }

        [Test]
        public async Task ParagraphServiceShouldReturnExistingItem()
        {
            // Arrange
            var firstParagraph = _existingParagraphs.FirstOrDefault();

            // Act
            var result = await _paragraphService.GetAsync(firstParagraph.Id);

            // Assert
            Assert.That(result != null, "Service should return a paragraph.");
            Assert.AreEqual(result.Id, firstParagraph.Id, "Service should return a paragraph with required Id.");
        }

        [Test]
        public async Task ParagraphServiceShouldReturnAllItems()
        {
            // Arrange
            var existingParagraphsCount = _existingParagraphs.Count;

            // Act
            var result = await _paragraphService.GetAllAsync();

            // Assert
            Assert.AreEqual(result.Count, existingParagraphsCount, "Service should return all existing paragraphs.");
        }

        private void SetupExistingParagraphs()
        {
            _existingParagraphs = new List<ParagraphEntity>()
            {
                new ParagraphEntity{ Id = "1", Content = "My first paragraph" },
                new ParagraphEntity{ Id = "2", Content = "My second paragraph" },
                new ParagraphEntity{ Id = "3", Content = "My third paragraph" }
            };
        }

        private void SetupMapper()
        {
            _mapperMock
                .Setup(_ => _.Map<ParagraphEntity>(It.IsAny<Paragraph>()))
                .Returns<Paragraph>(_ => new ParagraphEntity { Id = _.Id, Content = _.Content });

            _mapperMock
                .Setup(_ => _.Map<Paragraph>(It.IsAny<ParagraphEntity>()))
                .Returns<ParagraphEntity>(_ => new Paragraph { Id = _.Id, Content = _.Content });

            _mapperMock
                .Setup(_ => _.Map<List<Paragraph>>(It.IsAny<List<ParagraphEntity>>()))
                .Returns<List<ParagraphEntity>>(list =>
                {
                    return list.Select(_ => new Paragraph { Id = _.Id, Content = _.Content }).ToList();
                });
        }

        private void SetupParagraphRepository()
        {
            _paragraphRepositoryMock
                .Setup(_ => _.DeleteAsync(It.IsAny<string>()))
                .Returns<string>((idToDelete) => Task.FromResult<bool>(_existingParagraphs.RemoveAll(_ => _.Id == idToDelete) > 0));

            _paragraphRepositoryMock
                .Setup(_ => _.AddAsync(It.IsAny<ParagraphEntity>()))
                .Returns<ParagraphEntity>((newParagraphEntity) =>
                {
                    _existingParagraphs.Add(newParagraphEntity);
                    return Task.CompletedTask;
                });

            _paragraphRepositoryMock
                .Setup(_ => _.ReplaceAsync(It.IsAny<ParagraphEntity>()))
                .Returns<ParagraphEntity>((updatedParagraphEntity) => 
                {
                    var deletedItemsCount = _existingParagraphs.RemoveAll(_ => _.Id == updatedParagraphEntity.Id);
                    _existingParagraphs.Add(updatedParagraphEntity);
                    var result = new ReplaceOneResult.Acknowledged(deletedItemsCount, deletedItemsCount, deletedItemsCount);
                    return Task.FromResult(result as ReplaceOneResult);
                });

            _paragraphRepositoryMock
                .Setup(_ => _.GetAsync(It.IsAny<string>()))
                .Returns<string>((id) => Task.FromResult(_existingParagraphs.FirstOrDefault(_ => _.Id == id)));

            _paragraphRepositoryMock
                .Setup(_ => _.GetAllAsync())
                .Returns(Task.FromResult(_existingParagraphs.ToList()));
        }
    }
}
