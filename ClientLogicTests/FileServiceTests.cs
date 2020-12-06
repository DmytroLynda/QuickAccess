using ClientLogic;
using ClientLogic.ExternalInterfaces;
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
        private ILocalDeviceContext _fakeLocalDeviceContext;
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
            _fakeLocalDeviceContext = Mock.Of<ILocalDeviceContext>();

            var deviceContextFactoryMock = Mock.Get(_fakeDeviceContextFactory);
            deviceContextFactoryMock.Setup(factory => factory.GetDeviceContext(_fakeDevice)).Returns(_fakeDeviceContext);
            deviceContextFactoryMock.Setup(factory => factory.GetLocalDevice()).Returns(_fakeLocalDeviceContext);
        }

        [Test]
        public void DownloadFile_SavesFileOnLocalDevice()
        {
            //Arrange
            var fakeFile = Mock.Of<File>();
            Mock.Get(_fakeDeviceContext).Setup(context => context.DownloadFileAsync(_fakeFilePath)).ReturnsAsync(fakeFile);

            var fileService = new FileService(_fakeLogger, _fakeDeviceContextFactory);

            //Act
            var task = fileService.DownloadFileAsync(_fakeDevice, _fakeFilePath);
            task.Wait();

            //Assert
            Mock.Get(_fakeDeviceContextFactory).Verify(factory => factory.GetDeviceContext(_fakeDevice), Times.Once,
                $"{nameof(fileService.DownloadFileAsync)} should get {nameof(IDeviceContext)} from {nameof(IDeviceContextFactory)}.");

            Mock.Get(_fakeDeviceContextFactory).Verify(factory => factory.GetLocalDevice(), Times.Once,
                $"{nameof(fileService.DownloadFileAsync)} should get the {nameof(ILocalDeviceContext)} from {nameof(IDeviceContextFactory)}.");

            Mock.Get(_fakeDeviceContext).Verify(context => context.DownloadFileAsync(_fakeFilePath), Times.Once,
                $"{nameof(fileService.DownloadFileAsync)} should download the file from {nameof(IDeviceContext)}, which was got from {nameof(IDeviceContextFactory)}.");

            Mock.Get(_fakeLocalDeviceContext).Verify(localDevice => localDevice.SaveFile(fakeFile), Times.Once,
                $"{nameof(fileService.DownloadFileAsync)} should save a file, which was got from {nameof(IDeviceContext)} to {nameof(ILocalDeviceContext)}");
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
                $"{nameof(fileService.DownloadFileAsync)} should get {nameof(IDeviceContext)} from {nameof(IDeviceContextFactory)}.");

            Mock.Get(_fakeDeviceContext).Verify(deviceContext => deviceContext.GetFileInfoAsync(_fakeFilePath), Times.Once,
                $"{nameof(fileService.DownloadFileAsync)} should get {nameof(FileInfo)} from {nameof(IDeviceContext)}.");

            Assert.That(result, Is.EqualTo(expectedFileInfo), "The real result and expected value should be equal.");
        }
    }
}
