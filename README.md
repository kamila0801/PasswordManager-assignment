# PasswordManager

## Screenshots

![Screenshot 2023-10-09 at 14.21.34.png](screenshots/Screenshot%202023-10-09%20at%2014.21.34.png)
![Screenshot 2023-10-09 at 14.22.12.png](screenshots/Screenshot%202023-10-09%20at%2014.22.12.png)

## How to run the application
### Frontend:
- Step 1 - Go to folder “password-manager-fr” in terminal
- Step 2 - run npm install
- Step 3 - run ng serve

### Backend:
- Step 1 - Open the .snl file with your IDE
- Step 2 - Build the solution
- Step 3 - Then run project called “PasswordManager_Rest”
- Step 4 - This should successfully initiate the local SqLite database and generate basic entity.
You can create account through the frontend or swagger that can be accessed here: https://localhost:5028/swagger/index.html.

## Discussion

### Human Factor

Essentially, the entire security of users' credential chain relies on a single master password, which is not entirely ideal. Users might forget the password, share it on insecure platforms, or use a weak password, jeopardizing the security of the data.

There are a couple of ways to address some of these concerns, such as enforcing the use of a secure password when creating the account. While this enhances the security of the keychain, it also increases the risk of users forgetting their password. This is typically not a problem on other platforms with a "forgot password" option, but in our case, the encryption of the password is based on the master password. We would be unable to recreate stored credentials if the password is lost.

### Web Interaction

Since our solution is designed to work with web-based frontends, users will be entering their passwords in plain text in the browser every time. If malicious software is installed in the browser or on the machine, the password might be stolen, rendering the rest of the encryption process useless.

### AES vs SHA (Hashing Algorithms)

SHA algorithms are a suitable solution for storing data that is hashed predictably, meaning the same input always results in the same output. However, this doesn't work in our case because SHA is a one-way function, and we need the ability to run the encryption process both ways.

### Possible Solutions for Improvement

The fact that the loss of the master password would result in the loss of all the user's stored data without the possibility of recovery is alarming. Another possible solution could be as follows: instead of the master password being a single entity, it could be a combination of unique attributes the user possesses but cannot lose easily. For example, a phone number, personal questions, or the MAC address of the devices used to log into the system could be combined. A correct combination of these could be the key to a different database, which would return the seed used to encrypt the data. This way, the loss of one could be compensated for by the correct values for the others, solving them like a set of simultaneous equations.

## Conclusion

The proposed solution relies on a robust authentication system using the SHA256 one-way hashing function and AES encryption to secure users' credentials. While the database does not store the master password, and the PasswordUnits are encrypted using AES, the system has vulnerabilities.

The human factor introduces a significant risk to the security of this system. Users may forget their master password, share it on insecure platforms, or use weak passwords. Although enforcing secure password creation can help, it also increases the chances of a user forgetting their master password, resulting in the loss of all stored credentials.

In conclusion, while the proposed authentication system provides a secure method for storing and encrypting user credentials, it is susceptible to human error and potential security breaches at the web-based frontend. Addressing these vulnerabilities and implementing a more robust and fault-tolerant recovery mechanism can enhance the system's security and user-friendliness.