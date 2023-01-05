
workspace {
    model {
        InstagramUser = person "Instagram User" "An user of the instagram, with personal instagram account."
        LinkedFBUser = person "Linked FB User" "Use instagram with facebook account"

        enterprise "IG System" {            
            Facebook = softwareSystem "Facebook" "some functions to share and interact between the two systems" "Facebook"
            
            InstagramSystem = softwareSystem "Instagram System" "Allow user to view infomation about their Instagram accounts, activities interact posts and make interact with peoples" {
                webApplication = container "Web Application" "Delivers the static content and the Internet Instagram single page application." "Dotnet core and 'Repoitory Pattern'" "WebBrowser"
                mobileApp = container "Mobile App" "Provides a limited subset of the Internet Instagram functionality to customers via their mobile device." "Reactjs" "MobileApp"
                singlePageApplication = container "Single-Page Application" "Provides all of the internet instagram functionality and implementation that load only a single web browser" "JavaScript and Reactjs" "singlePageApplication"
                database = container "Database" "Stores user registation infomation, access log, etc." "Sql server" "Database"

                apiApplication = container "API Application" "Provides application infomation and functionality via a JSON/HTTPS API" "Dotnet core and 'Repository Pattern'" "apiApplication" {
                    // controllers = component "Controllers" "Directional, Provides an api interface, allowing users to use system functionality" "Dotnet core controller" 
                    // services = component "Services" "Connection between controller and repository, operations do not interact with database" "Dotnet Core - Framework entity"
                    // repositories = component "Repositories" "Direct interaction with the database, CRUD" "Framework Entity(LinQ) - 'repository pattern'"
                    // facebookConnector = component "Facebook Connector" "Request - check authentication, approve permission and accept token" "Dotnet Core - JS/Json"

                    // entities = component "Entities" "Provider info entities, direct mapping with database" "framework Entity"
                    authController = component "Auth Controller" "Authentication and authorizition for user, post, message v,v" "Dotnet core - Json Web Token"
                    

                    controllerUSers = component "UserController" "Directional, Provides API to manage users and interactions where a user follows or unfollows another user" "Dotnet Core Controller"
                    controllerPosts = component "PostController" "Directional, Provides API to allows various posts (photo, video, etc.), interact with posts" "Dotnet Core Controller"
                    controllerMedia = component "MediaController" "Directional, Provides API to allows storage and retrieval of photos and videos from its underlying data store" "Dotnet Core Controller"
                    controllerMessages = component "MessagerController" "Directional, Show all chats, messages and settings" "Dotnet Core Controller"

                    serviceUsers = component "UserService" "Intermediate class, call the methods in the UserRepository to execute query" "Dotnet Core"
                    servicePosts = component "PostService" "Intermediate class, call the methods in the PostRepository to execute query" "Dotnet Core"
                    serviceMedia = component "MediaService" "Intermediate class, call the methods in the MediaRepository to execute query" "Dotnet Core"
                    serviceMessages = component "MessageService" "Intermediate class, call the methods in the MessageRepository to execute query" "Dotnet Core"
                    serviceFbConnector =  component "FbConnectorService" "Intermediate class, Service to interact with FaceBook systems" "Dotnet Core"
                    serviceEmails = component "EmailService" "Send Email for some sistuation as forgotpassword or authorize accout" "MailKit/MimeKit"

                    repositoryUSers = component "UserRepository" "Query directly with db, all method names conform to naming conventions with interface so can extended" "Framework Entity(LinQ) - 'repository pattern'"
                    repositoryPosts = component "PostRepository" "Query directly with db, all method names conform to naming conventions with interface so can extended" "Framework Entity(LinQ) - 'repository pattern'"
                    repositoryMedia = component "MediaRepository" "Query directly with db, all method names conform to naming conventions with interface so can extended" "Framework Entity(LinQ) - 'repository pattern'"
                    repositoryMessages = component "MessagerRepository" "Query directly with db, all method names conform to naming conventions with interface so can extended" "Framework Entity(LinQ) - 'repository pattern'"


                }
                
            }
        } 

        # relationships between person and software systems
        InstagramUser -> InstagramSystem "Uses"
        LinkedFBUser -> InstagramSystem "Uses"
        InstagramSystem -> Facebook "Share and interact"
        
        
        # realationships to/from containers 
        InstagramUser -> webApplication "Visits instagram.com using" "HTTPS"
        InstagramUser -> mobileApp "Views account infomation and make activities using"
        InstagramUser -> singlePageApplication "Views account infomation and make activities using"

        LinkedFBUser -> webApplication "Visits instagram.com using" "HTTPS"
        LinkedFBUser -> mobileApp "Views account infomation and make activities using"
        LinkedFBUser -> singlePageApplication "Views account infomation and make activities using"

        apiApplication -> database "Reads from and writes to" "Framework Entity" 
        singlePageApplication -> apiApplication "Makes API calls to" "JSON/HTTPS"
        webApplication -> singlePageApplication "Deliver to the user web browser"
        mobileApp -> apiApplication "Makes API calls to" "JSON/HTTPS"
        apiApplication -> Facebook "Access token and Call to HTTPs to share stories"


        #ralationships of component - API Application
        // controllerUSers    -> authController "Authorizes access for records [HTTPs]"
        // controllerPosts    -> authController "Authorizes access for records [HTTPs]"
        // controllerMedia    -> authController "Authorizes access for records [HTTPs]"
        // controllerMessages -> authController "Authorizes access for records [HTTPs]"

        mobileApp -> authController "Uses"
        singlePageApplication -> authController "Uses"
        authController -> serviceUsers "Uses"
        authController -> serviceEmails "Uses"

        controllerUSers -> serviceUsers "Uses"
        controllerPosts -> servicePosts "Uses"
        controllerMedia -> serviceMedia "Uses"
        controllerMessages -> serviceMessages "Uses"
        controllerPosts -> serviceFbConnector "Uses"

        serviceUsers -> repositoryUSers "Uses"
        servicePosts -> repositoryPosts "Uses"
        serviceMedia -> repositoryMedia "Uses"
        serviceMessages -> repositoryMessages "Uses"
        serviceFbConnector -> Facebook "Share information stories and other to" "HTTPs"
        serviceFbConnector -> repositoryMedia "data transfer to store interaction history"

        repositoryUSers -> database "Uses"
        repositoryPosts -> database "Uses"
        repositoryMedia -> database "Uses"
        repositoryMessages -> database "Uses"

        singlePageApplication -> controllerUSers    "Make API call to" "Json/HTTPs"
        singlePageApplication -> controllerPosts    "Make API call to" "Json/HTTPs"
        singlePageApplication -> controllerMedia    "Make API call to" "Json/HTTPs"
        singlePageApplication -> controllerMessages "Make API call to" "Json/HTTPs"

        mobileApp -> controllerUSers    "Make API call to" "Json/HTTPs"
        mobileApp -> controllerPosts    "Make API call to" "Json/HTTPs"
        mobileApp -> controllerMedia    "Make API call to" "Json/HTTPs"
        mobileApp -> controllerMessages "Make API call to" "Json/HTTPs"


    }
    views {
        systemContext InstagramSystem "IGSystemContext" {
            include * 
            animation {

            }
            autoLayout
        }

        container InstagramSystem "ContainersIGSystem" {
            include * 
            animation {

            }
            autoLayout
        }

        component apiApplication "ComponentApiApplication" {
            include *
            animation {

            }
            autoLayout
        }


        styles {
            element "SoftwareSystem" {
                background #1168bd
                color #ffffff
            }
            element "Person" {
                shape person
                background #08427b
                color #ffffff
            }
            element "Facebook" {
                background #2caff5
                color #ffffff
            }
            element "MobileApp" {
                shape MobileDevicePortrait
                background #0ed7e6
            }
            element "WebBrowser" {
                shape WebBrowser
                background #0ed7e6
            }
            element "singlePageApplication" {
                background #0ed7e6
            }
            element "apiApplication" {
                background #0ed7e6 
            }
            element "Database" {
                shape Cylinder
                background #1168bd
                color #ffffff
            }
        }
    }
}

//Level 4: code [https://app.diagrams.net/#G1IJk4MwnQBbCbRh_iD-uZ8uYNSeemdxAx]

// Share with fb : 
// 1: Client requests access and permissions via SDK and login Dialog
// 2: User authenticates and approves permission
// 3: Access token is returned to client 

