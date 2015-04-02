using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Amazon.S3;
using Amazon.S3.Model;

namespace LWMI.Services
{
    public class S3Service
    {
        private static readonly string _awsAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];

        private static readonly string _awsSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];

        private static readonly string _bucketName = ConfigurationManager.AppSettings["Bucketname"];

        public static string GetData(string key)
        {
            try
            {
                IAmazonS3 client;
                using (client = Amazon.AWSClientFactory.CreateAmazonS3Client(_awsAccessKey, _awsSecretKey, Amazon.RegionEndpoint.EUWest1))
                {
                    var objectS3 = client.GetObject(new GetObjectRequest
                    {
                        BucketName = _bucketName,
                        Key = key
                    });
                    return new StreamReader(objectS3.ResponseStream).ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return "";
                //return ex.ToString();
            }  
        }

        public static void SetData(string key, string data)
        {
            try
            {
                IAmazonS3 client;
                using (client = Amazon.AWSClientFactory.CreateAmazonS3Client(_awsAccessKey, _awsSecretKey, Amazon.RegionEndpoint.EUWest1))
                {
                    var request = new PutObjectRequest()
                    {
                        BucketName = _bucketName,
                        CannedACL = S3CannedACL.PublicRead, //PERMISSION TO FILE PUBLIC ACCESIBLE
                        Key = key,
                        InputStream = new MemoryStream( Encoding.UTF8.GetBytes( data ) ) //SEND THE FILE STREAM
                    };
                    client.PutObject(request);
                }
            }
            catch (Exception ex)
            {
            }  
        }

        public static List<string> GetBucketList()
        {
            try
            {
                IAmazonS3 client;
                using (client = Amazon.AWSClientFactory.CreateAmazonS3Client(_awsAccessKey, _awsSecretKey, Amazon.RegionEndpoint.EUWest1))
                {                    
                    var objectS3 = client.ListObjects(new ListObjectsRequest() {BucketName = _bucketName});
                    return objectS3.S3Objects.Select(key => key.Key).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }  
        }
    }
}