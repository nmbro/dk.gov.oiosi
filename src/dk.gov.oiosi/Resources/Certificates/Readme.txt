

OiosiTestVOCES.pfx (NOT DEPLOYED)
-------------
VOCES test cert, should be installed from the certificate view 
in a mmc editor, under Local Computer\Trusted Root

Certificate name: TDC TOTALLØSNINGER A/S - TDC Test
Serial number: 40 36 4b 1e
Subject: OID.2.5.4.5=CVR:25767535-UID:1100080130597 + CN=TDC TOTALLØSNINGER A/S - TDC Test, O=TDC TOTALLØSNINGER A/S // CVR:25767535, C=DK
(NOTE: you may always use "OID.2.5.4.5" where you use "SERIALNUMBER".

password: "Test1234"


TDCTTestRoot.cer
----------------
Root cert, should be installed from the certificate view 
in a mmc editor, under Local Computer\Trusted Root
Serial number: 40 36 17 fc
This is also the root certificate for device certificates

TDCLiveOcesRoot.cer
-------------------
Live OCES root cert, should be installed from the certificate view 
in a mmc editor, under Local Computer\Trusted Root
Serial number: 3e 48 bd c4


Devicecertifikater ("NemHandelTest1.pfx" og "NemHandelTest2.pfx")
-----------------------------------------------------------------
Dette er de devicecertifikater som deployes til udviklere og test til 1/9.
Password for alle PKCS#12-filerne: Test1234

Certifikaterne er:

NemHandelTest1.pfx [anvendes til CLIENT side]
---------------------------------------------
Name: NemHandel Test 1
SN: 4036B376
CN=NemHandel Test 1 + SERIALNUMBER=CVR:26769388-DID:00000001, O=IT- og Telestyrelsen // CVR:26769388, C=DK

NemHandelTest2.pfx [anvendes til SERVER side]
---------------------------------------------
Name: NemHandel Test 2
SN: 4036B379
CN=NemHandel Test 2 + SERIALNUMBER=CVR:26769388-DID:00000002, O=IT- og Telestyrelsen // CVR:26769388, C=DK

Diverse vedr. devicecerts
--------------------------
DID er sat fortløbende til 00000001, 00000002, ... 00000008.
Tilhørende rodcertifikat findes på https://www.certifikat.dk/developer/storage/TDCOCESSTEST2.cer
Rodcertifikatet svarer til rodcertifikatet for øvrige OCES certifikater
