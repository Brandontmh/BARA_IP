using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//importing directives for authentication
using Firebase;
using Firebase.Auth; //Using Firebase Auth service
using Firebase.Extensions; //For threading purposes
using Firebase.Database; //Firebase real time database
using TMPro;//TextMesh Pro
using UnityEngine.UI;//UI handling 
using UnityEngine.SceneManagement;//scene loading
using System.Threading.Tasks;

using System.Text.RegularExpressions;


public class AuthManager : MonoBehaviour
{
    FirebaseAuth auth;

    public DatabaseReference dbReference;

    public TMP_InputField emailField;
    public TMP_InputField userField;
    public TMP_InputField passwordField;
    public GameObject signUpBtn;
    public GameObject forgetPasswordBtn;
    public GameObject signOutBtn;

    public string GetCurrentUsernameDisplay()
    {
        return auth.CurrentUser.DisplayName;
    }

    private bool loadScene = false;

    // Start is called before the first frame update

    void Awake()
    {
        InitalizeFirebase();
        loadScene = false;
    }

    void InitalizeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Update is called once per frame
    void Update()
    {
        if (loadScene == true)
        {
            SceneManager.LoadScene(1);
        }


    }

    public void SignUp()
    {
        string email = emailField.text.Trim();
        string password = passwordField.text.Trim();

        SignUpUser(email, password);
    }



    public void SignUpUser(string email, string password)
    {
        // automatically pass user info to the firebase project
        //attempt to create new user or check with there's already one
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            //perform task handling
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Sorry, there was an error creating your new account,ERROR: " + task.Exception);
                return;//exit from the attempt
            }
            else if (task.IsCompleted)
            {
                Firebase.Auth.FirebaseUser newPlayer = task.Result;
                Debug.LogFormat("Welcome to DDA Games {0}", newPlayer.Email);

                string username = userField.text;
                CreateNewSimplePlayer(newPlayer.UserId, username, username, newPlayer.Email);
                loadScene = true;
                //do anything you want after player creation eg. create new player
            }
        });
    }

    public void SignInUser()
    {
        string email = emailField.text.Trim();
        string password = passwordField.text.Trim();
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            //perform task handling
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Sorry, there was an issue signing in your account,ERROR: " + task.Exception);
                return;//exit from the attempt
            }
            else if (task.IsCompleted)
            {
                Firebase.Auth.FirebaseUser currentPlayer = task.Result;
                Debug.LogFormat("Welcome to game", currentPlayer.Email, currentPlayer.UserId);

                loadScene = true;

            }
        });
    }

    public void SignOutUser()
    {
        Debug.Log("Sign Out method...");
        if (auth.CurrentUser != null)
        {
            Debug.LogFormat("Auth user {0} {1}", auth.CurrentUser.UserId, auth.CurrentUser.Email);

            //get current index of a scene
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            auth.SignOut();
            if (currentSceneIndex != 0)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void ForgetPassword()
    {

        string email = emailField.text.Trim();

        auth.SendPasswordResetEmailAsync(email).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Sorry, there was an sending a password reset, ERROR: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                Debug.Log("Forget password email sent successfully...");
            }
        });
        Debug.Log("Forget password method...");
    }

    public bool ValidateEmail(string email)
    {
        bool isValid = false;
        //for all emails have @ 
        const string pattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";
        const RegexOptions options = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;

        if (email != "" && Regex.IsMatch(email, pattern, options))
        {
            isValid = true;
        }

        return isValid;
    }

    public bool ValidatePassword(string password)
    {
        bool isValid = false;

        //lenth of password at least 6 characters
        if (password != "" && password.Length >= 6)
        {
            isValid = true;
        }
        return isValid;
    }

    public void CreateNewSimplePlayer(string uuid, string displayName, string userName, string email)
    {
        SimpleGamePlayer newPlayer = new SimpleGamePlayer(displayName, userName, email);
        Debug.LogFormat("Player details : {0}", newPlayer.PrintPlayer());


        //root/players/$uuid 
        dbReference.Child("players/" + uuid).SetRawJsonValueAsync(newPlayer.SimpleGamePlayerToJson());
        Debug.Log("Added to database");

        UpdatePlayerDisplayName(displayName);
    }

    public void UpdatePlayerDisplayName(string displayName)
    {
        if (auth.CurrentUser != null)
        {
            UserProfile profile = new UserProfile
            {
                DisplayName = displayName
            };
            auth.CurrentUser.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("UpdateUserProfileAsync was cancelled");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("UpdateUserprofileAsync encountered an error: " + task.Exception);
                    return;
                }
                Debug.Log("User profile updated successfully");
            });
        }
    }

    public string GetCurrentUserDisplayName()
    {
        return auth.CurrentUser.DisplayName;
    }

    public FirebaseUser GetCurrentUser()
    {
        return auth.CurrentUser;
    }
}