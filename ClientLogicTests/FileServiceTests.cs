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
        private IDeviceContextFactory fakeDeviceContextFactory;
        private IDeviceContext fakeDeviceContext;
        private ILogger<IFileService> fakeLogger;

        [SetUp]
        public void SetUp()
        {
            fakeDeviceContextFactory = Mock.Of<IDeviceContextFactory>();
            fakeDeviceContext = Mock.Of<IDeviceContext>();
            fakeLogger = Mock.Of<ILogger<IFileService>>();
        }

        [Test]
        public void DownloadFile_SaveFileOnLocalDevice()
        {
            //Arrange
            var fakeDevice = Mock.Of<Device>();
            var deviceContextFactoryMock = Mock.Get(fakeDeviceContextFactory);
            deviceContextFactoryMock.Setup(factory => factory.GetDeviceContext(fakeDevice)).Returns(fakeDeviceContext);

            var fakeLocalDeviceContext = Mock.Of<ILocalDeviceContext>();
            deviceContextFactoryMock.Setup(factory => factory.GetLocalDevice()).Returns(fakeLocalDeviceContext);

            var deviceContextMock = Mock.Get(fakeDeviceContext);
            var fakeFilePath = new Mock<FilePath>(@"C:\someFile.txt").Object;
            var fakeFile = Mock.Of<File>();
            deviceContextMock.Setup(context => context.DownloadFileAsync(fakeFilePath)).ReturnsAsync(fakeFile);

            var fileService = new FileService(fakeLogger, fakeDeviceContextFactory);

            //Act
            var task = fileService.DownloadFileAsync(fakeDevice, fakeFilePath);
            task.Wait();

            //Assert
            deviceContextFactoryMock.Verify(factory => factory.GetDeviceContext(fakeDevice), Times.Once,
                $"{nameof(fileService.DownloadFileAsync)} should get {nameof(IDeviceContext)} from {nameof(IDeviceContextFactory)}.");

            deviceContextFactoryMock.Verify(factory => factory.GetLocalDevice(), Times.Once,
                $"{nameof(fileService.DownloadFileAsync)} should get the {nameof(ILocalDeviceContext)} from {nameof(IDeviceContextFactory)}.");

            deviceContextMock.Verify(context => context.DownloadFileAsync(fakeFilePath), Times.Once,
                $"{nameof(fileService.DownloadFileAsync)} should download the file from {nameof(IDeviceContext)}, which was got from {nameof(IDeviceContextFactory)}.");

            Mock.Get(fakeLocalDeviceContext).Verify(localDevice => localDevice.SaveFile(fakeFile), Times.Once,
                $"{nameof(fileService.DownloadFileAsync)} should save a file, which was got from {nameof(IDeviceContext)} to {nameof(ILocalDeviceContext)}");
        }
    }
}
