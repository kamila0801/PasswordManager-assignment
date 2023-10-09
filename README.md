# PasswordManager_DB_FO
PasswordManager_DB_FO


Instructions to run the application.
Frontend:
Step 1 - Go to folder “password-manager-fr”  in terminal
Step 2 -npm install
Step 3 - ng serve.




Backend:
Step 1 - Open the .snl file with your ide
Step 2 - Build the solution
Step 3 - Then run project calle “PasswordManager_Rest”
Step 4 - This should successfully initiate the local SqLite database and generate basic entity
You can create account through the frontend or additionally through the swagger that should automatically open once the project is built.
We experienced some problems with ensure deleted and ensure created so if you need to re run the project for some reason you need to delete the db file that would be located in the Rest project.

If swagger didn’t automatically opened you can open swagger by using this link:
http://localhost:5191/swagger/index.html

Discussion

Human factor 

Basically the entire security of all of users credential chain is dependent on one master password which is not entirely optimal. User might forget the password, share it on some hostile rescourses or simply use a not secure password which would compromise the security of the data.

There are couple of ways to mitigate some of thise concerns like for example enforce a use of secure password when creating the account. This increases the security of the keychain but also amplifies the risk of user forgetting the password. Usually that is not an issue on other platforms with forgot password option, but in our case the encryption of the password is based on the Master password. We would simply be unable to recreate stored credentials if the password is lost.

Interaction in the web

As our solution is meant to work with the web based frontend, user will be writing it in plain text in the browser every time. If there were to be some malicious software installed in the browser or on the machine in general, that password might be stole, rendering the rest of the encryption processuseless.

AES vs SHA (family of hashing algorithms)

SHA algorithms are a great solution for storing the data that is hashed in a predictable manner, meaning the same input would always result in the same output. This though doesn’t  works in our case because SHA is a one-way function and we need the ability to to run the encryption process back and forth.

Possible ways to improve solution

The fact that in our case the loss of the MasterPassword would result in the loss of all the data that user was storing without the possibility to reclaim th e lost data is rather frightening and possible other solution could be as follows.
The MasterPassword instead of being one entity liek password could instead be the combination of unique attributes user has but cannot loose in a normal fashion. Like for example phone number, personal questions (maybe a mac address of the devices he uses to login into the system). A correct combination of those could be the key to a different database which would return the seed used to encrypt the data. This way the loss of one of them could be saved by the correct values for the other ones, those could be solved like a set of simultaneous equations.  

 
Conclusion

The proposed solution relies on a strong authentication system using SHA256 one-way hashing function and AES encryption to secure users' credentials. While the database does not store the master password, and the PasswordUnits are encrypted using AES, the system has some vulnerabilities.

The human factor presents a significant risk to the security of this system. Users may forget their master password, share it on insecure platforms, or use weak passwords. Although enforcing secure password creation can help, it also increases the chances of a user forgetting their master password, which would result in the loss of all stored credentials.

In conclusion, while the proposed authentication system provides a secure method for storing and encrypting user credentials, it is vulnerable to human error and potential security breaches at the web-based frontend. By addressing these vulnerabilities and implementing a more robust and fault-tolerant recovery mechanism, the system can become more secure and user-friendly.
