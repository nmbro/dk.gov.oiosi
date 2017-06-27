Simple Howto:

First create the selfSigned CA: 
 - DigstSelfSignedCA.txt

Then create the selfSigned Software Publisher Certificate (SPC): 
 - DigstSelfSignedSPC.txt

For the build scrip to work, the Thumbprint of the certificate must exist in the System Enviroment Variable
Key  :  DigstSPCThumbprint
Value:  42e8fb16960b7533d84dc4a5450a0eafe96235ce


