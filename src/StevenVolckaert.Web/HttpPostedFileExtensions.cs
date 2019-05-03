namespace StevenVolckaert.Web
{
    using System;
    using System.Web;

    /// <summary>
    ///      Provides extension methods for <see cref="HttpPostedFile"/> instances.
    /// </summary>
    public static class HttpPostedFileExtensions
    {
        /// <summary>
        ///     Converts the contents of the file to an array of bytes.
        /// </summary>
        /// <param name="httpPostedFile">
        ///     The <see cref="HttpPostedFile"/> instance this extension method affects.
        /// </param>
        /// <returns>
        ///     An array of bytes representing the data contained in <paramref name="httpPostedFile"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="httpPostedFile"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="OverflowException">
        ///     <paramref name="httpPostedFile"/> represents a file that's larger than 2 GiB.
        /// </exception>
        public static byte[] ToByteArray(this HttpPostedFile httpPostedFile)
        {
            if (httpPostedFile == null)
                throw new ArgumentNullException(nameof(httpPostedFile));

            if (httpPostedFile.InputStream.Length > int.MaxValue)
                throw new OverflowException(
                    $"File input stream length is larger than System.Int32.MaxValue." +
                    $"Argument '{nameof(httpPostedFile)}' represents a file that's larger than 2 GiB."
                );

            var inputStreamLength = Convert.ToInt32(httpPostedFile.InputStream.Length);
            var byteArray = new byte[inputStreamLength];
            httpPostedFile.InputStream.Read(byteArray, 0, inputStreamLength);

            return byteArray;
        }
    }
}
