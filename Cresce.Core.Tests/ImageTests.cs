using NUnit.Framework;

namespace Cresce.Core.Tests
{
    public class ImageTests
    {
        [Test]
        public void Images_can_be_converted_between_byte_array_and_base_64()
        {
            var base64Image = GetBase64Image();
            var imageBase64 = new Image(base64Image);
            var imageByteArray = new Image(imageBase64.ToByteArray());

            Assert.Multiple(() =>
            {
                Assert.That(imageBase64.ToByteArray(), Is.EqualTo(imageByteArray.ToByteArray()));
                Assert.That(imageBase64.ToBase64(), Is.EqualTo(base64Image));
                Assert.That(imageByteArray.ToBase64(), Is.EqualTo(base64Image));
            });
        }

        [Test]
        public void Malformed_image_returns_empty_base64([Values(null, "")] string image)
        {
            var imageBase64 = new Image(image);

            Assert.That(imageBase64.ToBase64(), Is.EqualTo(string.Empty));
        }

        [Test]
        public void Null_byte_array_returns_empty_base64()
        {
            var imageBase64 = new Image((byte[]) null);

            Assert.That(imageBase64.ToBase64(), Is.EqualTo(string.Empty));
        }

        private static string GetBase64Image() =>
            "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";
    }
}