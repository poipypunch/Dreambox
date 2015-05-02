using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace MVCDreambox.App_Code
{
	/// <summary>
	/// Summary description for RSACrypto.
	/// </summary>
	public class RSACrypto
	{	
		#region "Member"

		//Ppublic modulus and exponent used in encryption
		//private RSAParameters rsaParamsExcludePrivate ;

		//Public and private RSA params use in decryption
		//private RSAParameters rsaParamsIncludePrivate ;	

		//Public Key
		private string publicOnlyKeyXml = null ;

		// Private key
		private string publicPrivateKeyXml = null ;

		// RSACryptoServiceProvider
		RSACryptoServiceProvider rsaProvider = null ;
			
		//
		// Default key pair
		//

		//Public Key
		public const string publicXmlKey = "<RSAKeyValue><Modulus>rVos2OJuRXD+UncfiQc3ld7XEvUyfUMkIlZs0nh1uLcCLnUAdveiSTVTa0b/xAiXAR+AhFXxgpkPqz7RCQTSy1D0S+06K75ygmeB5Zj+ki2/ySAguQ01FfJ09j3oVOuy9cTlIx3tOsRNDn9wClfllqbJmQSYnrgYhNZzPR9lXJM=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>" ;

		// Private key
		private string privateXmlKey = "<RSAKeyValue><Modulus>rVos2OJuRXD+UncfiQc3ld7XEvUyfUMkIlZs0nh1uLcCLnUAdveiSTVTa0b/xAiXAR+AhFXxgpkPqz7RCQTSy1D0S+06K75ygmeB5Zj+ki2/ySAguQ01FfJ09j3oVOuy9cTlIx3tOsRNDn9wClfllqbJmQSYnrgYhNZzPR9lXJM=</Modulus><Exponent>AQAB</Exponent><P>43IN1PYdqf4mrZvfTUHpJzG4Odpk2mV8lAwbwTI7Khyr/nSPnTK7J3GYkBopt7KmzxZGzQBdtkgzgLybt+7dfQ==</P><Q>wx2Zg53Oh1z2KXkOUP/kiVULfRZ8JDbJU/+jbHB3Woaayk15Sten7RmkASW6NMqnX5w1tJqZlB6LJPfYoWZ/Tw==</Q><DP>zvLlEQPLo/RjXw331YUGTypXNRS5NVqoStDlpBk/aibifEm0rtUNI2uh5GRZ1xKP4tejHj6aBhSWACodCfQmxQ==</DP><DQ>jNg1FLk5eKU2XMGx/+54PlpRrL4qZsnVkvkaMxEMVyCLZDWAtPHvmkvEA8AEJk+TeoT8j5559l4F+22dxwSCKQ==</DQ><InverseQ>xWUfNxpByKvH3CFnXwQqh9e1EDFvQ5SRjgP4yI1tax2WXSuw5lt4zSelWRKemCLQ/qYMiV0iGWtxgFs7P5IFBw==</InverseQ><D>LHuUQH8yDq2lBSI+PFpWwCVziRDUSOJetlT5DbUNeD212Jf7a4u14BVH96I7ZWFS5l3gC5VGKN3/8FBpMWhEcq0/sB/Y3TvmQhCidauonr8HT8UjGsz8F35ka1XImJjbpGpc04/6SqB2GtmFD4bGNrpiK9XNIxZ55MKDYl+4oUk=</D></RSAKeyValue>" ;
			

		public string PublicKey
		{
			get { return this.publicOnlyKeyXml; }
		}

		/*public string PrivateKey
			{
				get { return this.publicPrivateKeyXml; } 
			}*/

		#endregion

		#region "RSACrypto Constructor."
		public RSACrypto()
		{
			// Default generate new RSAParams
			//GenerateNewRSAParams();
			this.publicOnlyKeyXml = publicXmlKey ;
			this.publicPrivateKeyXml = privateXmlKey ;
		}
		#endregion

		#region "GenerateNewRSAParams method"
		// Create Key pair
		public void GenerateNewRSAParams()    
		{
			//Create new instance of  RSACryptoServiceProvider
			rsaProvider = new RSACryptoServiceProvider();
				   
			//Provide public and private RSA params
			//rsaParamsIncludePrivate =   rsaProvider.ExportParameters(true);
				
			// Set private key
			this.publicPrivateKeyXml = rsaProvider.ToXmlString(true);

			//Provide public only RSA params
			//rsaParamsExcludePrivate = rsaProvider.ExportParameters(false);
				
			//Set public key
			this.publicOnlyKeyXml = rsaProvider.ToXmlString(false);
		}
		#endregion
			
		#region "ExportXmlKeyPair method"

		// Export key pair to specific destination in xml file format
		public void ExportXmlKeyPair( string destinationPath)
		{	
			string path = null ;
			path = this.GetCorrectPath(destinationPath) ;
			try 
			{
				//Provide public and private RSA params and write to Xml file format
				StreamWriter writer = new StreamWriter(path+"/PublicPrivateKey.xml");				
				writer.Write(this.publicPrivateKeyXml);
				writer.Close();				

				//Provide public only RSA params  and write to Xml file format
				writer = new StreamWriter(path+"/PublicOnlyKey.xml");				
				writer.Write(this.publicOnlyKeyXml);
				writer.Close();
			}
			catch ( Exception ex ) 
			{
				throw new Exception(ex.Message);
			}
		}

		#endregion

		#region "GetCorrectPath method"

		private string GetCorrectPath ( string path ) 
		{
			if ( path.LastIndexOf("/")!= -1)
				return path;
			else
				return path.Substring(0,path.Length-1);
		}

		#endregion

		#region  "GetPublicOnlyKeyFromXml method"

		// Get public key from Xml file
		public void GetPublicOnlyKeyFromXml(string publicXmlFileName) 
		{	
			string publicOnlyKeyXml = null ;
			try 
			{
				//Public only RSA parameters for encrypt
				StreamReader reader = new StreamReader(publicXmlFileName);				
				publicOnlyKeyXml = reader.ReadToEnd(); 
				reader.Close();
					
				this.publicOnlyKeyXml = publicOnlyKeyXml ;
			}
			catch (Exception ex ) 
			{
				throw new Exception(ex.Message);
			}			
		}

		#endregion

		#region "GetPrivateKeyFromXml method

		// Get prvate key from Xml fiel for decrypting
		public void GetPrivateKeyFromXml( string privateXmlFileName)
		{
			string publicPrivateKeyXml = null ;
			try 
			{
				// Complete RSA parameters for encrypt
				StreamReader reader = new StreamReader(privateXmlFileName);
				publicPrivateKeyXml = reader.ReadToEnd();
				reader.Close();
				this.publicPrivateKeyXml = publicPrivateKeyXml ;
					
			}
			catch ( Exception ex ) 
			{
				throw new Exception(ex.Message);
			}
		}
		#endregion
			
		#region "Encryption "

		// Overloding encrypt method
		public string Encrypt(string plaintext)
		{
			try 
			{			
				//Create new instance of  RSACryptoServiceProvider
				RSACryptoServiceProvider rsa =  new RSACryptoServiceProvider();

				//Public only RSA parameters for encrypt
				//StreamReader reader = new StreamReader("PublicOnlyKey.xml");				
				//string publicOnlyKeyXml = reader.ReadToEnd();
				//reader.Close();	
				rsa.FromXmlString(this.publicOnlyKeyXml);				
				   
				//Read plaintext, encrypt it to ciphertext
				byte[] plainbytes = Encoding.ASCII.GetBytes(plaintext);

				return  Convert.ToBase64String(rsa.Encrypt( plainbytes,false)); //fOAEP needs high encryption pack		 
			}
			catch ( CryptographicException cx ) 
			{
				throw new CryptographicException( cx.Message) ;
			}
		}// End Encrypt method
			
		// Overloding encrypt method
		public string Encrypt( string plaintext, string publicKeyFromXmlString)
		{
			try 
			{
                CspParameters RSAParams = new CspParameters();
                RSAParams.Flags = CspProviderFlags.UseMachineKeyStore;
                //create new instance of  RSACryptoServiceProvider
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(RSAParams);
                ////Create new instance of  RSACryptoServiceProvider
                //RSACryptoServiceProvider rsa =  new RSACryptoServiceProvider();
					
				// Import parameter from Xml string
				rsa.FromXmlString(publicKeyFromXmlString);				
				   
				//Read plaintext, encrypt it to ciphertext
				byte[] plainbytes = Encoding.ASCII.GetBytes(plaintext);

				return  Convert.ToBase64String(rsa.Encrypt( plainbytes, false)); //fOAEP needs high encryption pack		 
			}
			catch ( CryptographicException cx ) 
			{
				throw new CryptographicException( cx.Message) ;
			}
		}// End overloading Encrypt method
			
		#endregion

		#region "Decryption "

		// Decrypt method
		public string Decrypt( string b64String ) 
		{	
			try 
			{
				byte[] ciphertext = Convert.FromBase64String(b64String);

                CspParameters RSAParams = new CspParameters();
                RSAParams.Flags = CspProviderFlags.UseMachineKeyStore;
				//create new instance of  RSACryptoServiceProvider
				RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(RSAParams);
					   
				//import public and private RSA parameters from Xml file
				//StreamReader reader = new StreamReader("PublicPrivateKey.xml");
				//string publicPrivateKeyXml = reader.ReadToEnd();
				//reader.Close();
				rsa.FromXmlString(this.publicPrivateKeyXml);				
					   
				//read ciphertext, decrypt it to plaintext
				byte[] plainbytes = rsa.Decrypt( ciphertext, false); //fOAEP needs high encryption pack
				return Encoding.ASCII.GetString(plainbytes) ;
			}
			catch ( CryptographicException cx ) 
			{
				throw new CryptographicException( cx.Message) ;
			}
		}// end Decrypt method

		// Overloading method
		public string Decrypt(string b64String ,string privateKeyFromXmlString ) 
		{
			try 
			{
				byte[] ciphertext = Convert.FromBase64String(b64String);

                CspParameters RSAParams = new CspParameters();
                RSAParams.Flags = CspProviderFlags.UseMachineKeyStore;
				//create new instance of  RSACryptoServiceProvider
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(RSAParams);
					   

				//import public and private RSA parameters from Xml file				
				rsa.FromXmlString(privateKeyFromXmlString);				
					   
				//read ciphertext, decrypt it to plaintext
				byte[] plainbytes = rsa.Decrypt( ciphertext, false); //fOAEP needs high encryption pack
				return Encoding.ASCII.GetString(plainbytes) ;
			}
			catch ( CryptographicException cx ) 
			{
				throw new CryptographicException( cx.Message) ;
			}
		}// end Decrypt method

		#endregion

	}// Eend RSACrypto class
}
