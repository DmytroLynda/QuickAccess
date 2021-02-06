using ClientLogic;
using ClientLogic.ExternalInterfaces;
using ClientLogic.Internal;
using DomainEntities;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace ClientLogicTests
{
    [TestFixture]
    public class FileServiceTests
    {
        private ILogger<IFileService> _fakeLogger;

        private IDeviceContextFactory _fakeDeviceContextFactory;
        private IDeviceContext _fakeDeviceContext;
        private Device _fakeDevice;
        private FilePath _fakeFilePath;

        [SetUp]
        public void SetUp()
        {
            _fakeDeviceContextFactory = Mock.Of<IDeviceContextFactory>();
            _fakeDeviceContext = Mock.Of<IDeviceContext>();
            _fakeLogger = Mock.Of<ILogger<IFileService>>();
            _fakeFilePath = new Mock<FilePath>(@"C:\someFile.txt").Object;
            _fakeDevice = Mock.Of<Device>();

            var deviceContextFactoryMock = Mock.Get(_fakeDeviceContextFactory);
            deviceContextFactoryMock.Setup(factory => factory.GetDeviceContext(_fakeDevice)).ReturnsAsync(_fakeDeviceContext);
        }

        [Test]
        public void DownloadFile_SavesFileOnLocalDevice()
        {
            //Arrange
            Mock.Get(_fakeDeviceContext).Setup(context => context.DownloadFileAsync(_fakeFilePath));

            var fileService = new FileService(_fakeLogger, _fakeDeviceContextFactory);

            //Act
            var task = fileService.DownloadFileAsync(_fakeDevice, _fakeFilePath);
            task.Wait();

            //Assert
            Mock.Get(_fakeDeviceContextFactory).Verify(factory => factory.GetDeviceContext(_fakeDevice), Times.Once,
                $"{nameof(fileService.DownloadFileAsync)} should get {nameof(IDeviceContext)} from {nameof(IDeviceContextFactory)}.");

            Mock.Get(_fakeDeviceContext).Verify(context => context.DownloadFileAsync(_fakeFilePath), Times.Once,
                $"{nameof(fileService.DownloadFileAsync)} should download the file from {nameof(IDeviceContext)}, which was got from {nameof(IDeviceContextFactory)}.");
        }

        [Test]
        public void GetFileInfo_ReturnsFileInfo()
        {
            //Arrange
            var expectedFileInfo = Mock.Of<FileInfo>();
            Mock.Get(_fakeDeviceContext).Setup(deviceContext => deviceContext.GetFileInfoAsync(_fakeFilePath)).ReturnsAsync(expectedFileInfo);

            var fileService = new FileService(_fakeLogger, _fakeDeviceContextFactory);

            //Act
            var task = fileService.GetFileInfoAsync(_fakeDevice, _fakeFilePath);
            var result = task.Result;

            //Assert
            Mock.Get(_fakeDeviceContextFactory).Verify(factory => factory.GetDeviceContext(_fakeDevice), Times.Once,
                $"{nameof(fileService.GetFileInfoAsync)} should get {nameof(IDeviceContext)} from {nameof(IDeviceContextFactory)}.");

            Mock.Get(_fakeDeviceContext).Verify(deviceContext => deviceContext.GetFileInfoAsync(_fakeFilePath), Times.Once,
                $"{nameof(fileService.GetFileInfoAsync)} should get {nameof(FileInfo)} from {nameof(IDeviceContext)}.");

            Assert.That(result, Is.EqualTo(expectedFileInfo));
        }

        [Test]
        public void ShowDirectory_ReturnsPathes()
        {
            //Arrange
            var expextedPathes = Mock.Of<List<Path>>();

            var fakeDirectoryPath = new Mock<DirectoryPath>(@"C:\SomeDirectory").Object;
            Mock.Get(_fakeDeviceContext).Setup(deviceContext => deviceContext.OpenFolderAsync(fakeDirectoryPath)).ReturnsAsync(expextedPathes);
            var fileService = new FileService(_fakeLogger, _fakeDeviceContextFactory);

            //Act
            var task = fileService.ShowDirectoryAsync(_fakeDevice, fakeDirectoryPath);
            var result = task.Result;

            //Assert
            Mock.Get(_fakeDeviceContextFactory).Verify(factory => factory.GetDeviceContext(_fakeDevice), Times.Once,
                $"{nameof(fileService.ShowDirectoryAsync)} should get {nameof(IDeviceContext)} from {nameof(IDeviceContextFactory)}.");

            Mock.Get(_fakeDeviceContext).Verify(deviceContext => deviceContext.OpenFolderAsync(fakeDirectoryPath), Times.Once,
                $"{nameof(fileService.ShowDirectoryAsync)} should get folder {nameof(Path)}es from {nameof(IDeviceContext)}.");

            Assert.That(result, Is.EqualTo(expextedPathes));
        }
    }
}
