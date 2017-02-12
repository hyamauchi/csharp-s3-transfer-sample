using Amazon.S3;
using Amazon.S3.Transfer;
using System;
using System.IO;
using System.Text;

namespace S3TransferSample
{
    class Program
    {
        static string existingBucketName = "*** Provide bucket name ***";
        static string keyName = "*** Provide your object key ***";
        static string awsAccessKeyId = "*** Provide your awsAccessKeyId ***";
        static string awsSecretAccessKey = "*** Provide your awsSecretAccessKey ***";

        static void Main(string[] args)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var sw = new StreamWriter(ms, Encoding.UTF8);
                try
                {
                    sw.Write("a,b,c");
                    sw.Flush();
                    ms.Seek(0, SeekOrigin.Begin);

                    try
                    {
                        var fileTransferUtility = new TransferUtility(awsAccessKeyId,
                            awsSecretAccessKey, Amazon.RegionEndpoint.APNortheast1);

                        fileTransferUtility.Upload(ms, existingBucketName, keyName);
                    }
                    catch (AmazonS3Exception s3Exception)
                    {
                        Console.WriteLine(s3Exception.Message, s3Exception.InnerException);
                    }
                }
                finally
                {
                    sw.Dispose();
                }
            }

        }
    }
}
