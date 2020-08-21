# Gameball .NET SDK
The Gameball .NET SDK provides convenient access to the Gameball API from applications written in the .NET language.
​
## Documentation
​
Please refer to the  [Gameball API docs](https://docs.gameball.co).
​
## Installation
​
You don't need this source code unless you want to modify the SDK. If you just
want to use the SDK, just run: 

​
Using the [.NET Core command-line interface (CLI) tools][dotnet-core-cli-tools]:

```sh
dotnet add package gameball
```

Using the [NuGet Command Line Interface (CLI)][nuget-cli]:

```sh
nuget install gameball
```

Using the [Package Manager Console][package-manager-console]:

```powershell
Install-Package gameball
```

From within Visual Studio:

1. Open the Solution Explorer.
2. Right-click on a project within your solution.
3. Click on *Manage NuGet Packages...*
4. Click on the *Browse* tab and search for "Gameball".
5. Click on the Gameball package, select the appropriate version in the
   right-tab and click *Install*.
   
### Supports
​
-   .NET Standard 2.0+
-   .NET Core 2.0+
-   .NET Framework 4.5+

### Requirements
​
-   NewtonSoft.Json 12.0+
-   NETStandard.Library 2.0+
​
## Usage
​
The SDK needs to be configured with your account's API & Transaction keys available in your [Gameball Dashboard](https://help.gameball.co/en/articles/3467114-get-your-account-integration-details-api-key-and-transaction-key)
​
```csharp
using Gameball;
var Gameball = new Gameball(apiKey,transactionKey);
```
​
### Example

#### Create Player
```csharp
//Example 1
using Gameball;
using Gameball.Models;
var Gameball = new Gameball(apiKey);
PlayerRequest request = new PlayerRequest() {
  PlayerUniqueId = "player123",
  PlayerAttributes = new PlayerAttributes() {
    DisplayName = "Jon Snow",
    FirstName = "Jon",
    LastName = "Snow",
    Email = "jon.snow@example.com",
    Gender = "M",
    DateOfBirth = new DateTime(1980, 9, 19),
    JoinDate = new DateTime(2019, 9, 19, 21, 6, 29, 158)
  }
};
//Adding Custom Attributes
request.PlayerAttributes.AddCustomAttribute("location", "Miami");
request.PlayerAttributes.AddCustomAttribute("graduationDate", new DateTime(2018, 7, 4, 21, 6, 29, 158));
request.PlayerAttributes.AddCustomAttribute("isMarried", false);

var player_response = Gameball.InitializePlayer(request);
```

#### Player Info
```csharp
//Example 1
using Gameball;
using Gameball.Models;
var Gameball = new Gameball(apiKey,transactionKey);

var player_info_response = Gameball.GetPlayerInfo("player123");
```
​
#### Sending an Event
​
```csharp
//Example 1
using Gameball;
using Gameball.Models;
var Gameball = new Gameball(apiKey);
//Creating Request
var request = new EventRequest()
{
	PlayerUniqueId = "player123"
};
//Place Order Event			
Event PlaceOrder = new Event()
{
	Name = "place_order"
};
PlaceOrder.AddMetadata("total_amount", "100");
PlaceOrder.AddMetadata("category", new string[] { "electronics", "cosmetics" });
//Review Event
Event Review = new Event()
{
	Name = "review"
};
//Sending Request
request.AddEvent(PlaceOrder);
request.AddEvent(Review);
var send_event_response = Gameball.SendEvent(request);


//Example 2
using Gameball;
using Gameball.Models;
var Gameball = new Gameball(apiKey);
//Creating Request
var request = new EventRequest()
{
	PlayerUniqueId = "player123",
	PlayerAttributes = new PlayerAttributes()
	{
	 DisplayName = "Jon Snow",
	 Email = "jon.snow@example.com",
	 DateOfBirth = new DateTime(1980,9,19),
	 JoinDate = new DateTime (2019,9,19,21,6,29,158)
	}
};
//Reserve Event			
Event Reserve = new Event()
{
	Name = "reserve"
};
Reserve.AddMetadata("rooms",2);

//Sending Request
request.AddEvent(Reserve);
var send_event_response = Gameball.SendEvent(request);

//Example 3
using Gameball;
using Gameball.Models;
var Gameball = new Gameball(apiKey);
//Creating Request
var request = new EventRequest()
{
	PlayerUniqueId = "player123",
	PlayerAttributes = new PlayerAttributes()
	{
	 DisplayName = "Jon Snow",
	 Email = "jon.snow@example.com",
	 DateOfBirth = new DateTime(1980,9,19),
	 JoinDate = new DateTime (2019,9,19,21,6,29,158)
	}
};
//Adding Custom Attributes
request.PlayerAttributes.AddCustomAttribute("location", "Miami");
request.PlayerAttributes.AddCustomAttribute("graduationDate", new DateTime(2018, 7, 4, 21, 6, 29, 158));
request.PlayerAttributes.AddCustomAttribute("isMarried", false);


//Reserve Event			
Event Reserve = new Event()
{
	Name = "reserve"
};
Reserve.AddMetadata("rooms",2);

//Sending Request
request.AddEvent(Reserve);
var send_event_response = Gameball.SendEvent(request);
			​ 
```

#### Referral
```csharp
//Example 1
using Gameball;
using Gameball.Models;
var Gameball = new Gameball(apiKey);

ReferralRequest request = new ReferralRequest()
{
	PlayerUniqueId = "player456",
	PlayerCode = "CODE11"
};
var create_referral_response = Gameball.CreateReferral(request);

//Example 2
using Gameball;
using Gameball.Models;
var Gameball = new Gameball(apiKey);

ReferralRequest request = new ReferralRequest()
{
	PlayerUniqueId = "player456",
	PlayerCode = "CODE11",
	PlayerAttributes = new PlayerAttributes()
		{
		 DisplayName = "Tyrion Lannister",
		 FirstName = "Tyrion",
		 LastName = "Lannister",
		 Email = "tyrion@example.com",
		 Gender = "M",
		 DateOfBirth = new DateTime(1978,1,11),
	     JoinDate = new DateTime (2019,9,19,21,6,29,158)
		 
		}
	};
//Adding Custom Attributes
request.PlayerAttributes.AddCustomAttribute("location", "Miami");
request.PlayerAttributes.AddCustomAttribute("graduationDate", new DateTime(2018, 7, 4, 21, 6, 29, 158));
request.PlayerAttributes.AddCustomAttribute("isMarried", false);
};
var create_referral_response = Gameball.CreateReferral(request);

```

#### Rewarding
```csharp
//Example 1
using Gameball;
using Gameball.Models;
var Gameball = new Gameball(apiKey,transactionKey);

RewardPointsRequest request = new RewardPointsRequest()
	{
		PlayerUniqueId = "player123",
		Amount = 99.98,
		TransactionId = "tra_123456789"
	};
var reward_points_response = Gameball.RewardPoints(request);

//Example 2
using Gameball;
using Gameball.Models;
var Gameball = new Gameball(apiKey,transactionKey);

RewardPointsRequest request = new RewardPointsRequest()
	{
		PlayerUniqueId = "player456",
		Amount = 2500,
		TransactionId = "tra_123456789",
		PlayerAttributes = new PlayerAttributes()
		{
		 DisplayName = "Tyrion Lannister",
		 FirstName = "Tyrion",
		 LastName = "Lannister",
		 Email = "tyrion@example.com",
		 Gender = "M",
		 DateOfBirth = new DateTime(1978,1,11),
	     JoinDate = new DateTime (2019,9,19,21,6,29,158)
		 
		}
	};
//Adding Custom Attributes
request.PlayerAttributes.AddCustomAttribute("location", "Miami");
request.PlayerAttributes.AddCustomAttribute("graduationDate", new DateTime(2018, 7, 4, 21, 6, 29, 158));
request.PlayerAttributes.AddCustomAttribute("isMarried", false);

var reward_points_response = Gameball.RewardPoints(request);

```
#### Player Balance
```csharp
//Example 1
using Gameball;
var Gameball = new Gameball(apiKey,transactionKey);

var get_balance_response = Gameball.GetPlayerBalance("player456");
```

#### Hold Points
```csharp
//Example 1
using Gameball;
using Gameball.Models;
var Gameball = new Gameball(apiKey,transactionKey);

HoldPointsRequest request = new HoldPointsRequest()
	{
		Amount = 98.89,
		PlayerUniqueId = "player456"
	};
var hold_points_response = Gameball.HoldPoints(request);
```

#### Redeem Points
```csharp
//Example 1
using Gameball;
using Gameball.Models;
var Gameball = new Gameball(apiKey,transactionKey);

RedeemPointsRequest request = new RedeemPointsRequest()
	{
	    PlayerUniqueId = "player456",
		TransactionId = "tra_123456789",
		HoldReference = "2342452352435234"
	};
var redeem_points_response = Gameball.RedeemPoints(request);
```

#### Reverse Transaction
```csharp
//Example 1
using Gameball;
using Gameball.Models;
var Gameball = new Gameball(apiKey,transactionKey);

ReverseTransactionRequest request = new ReverseTransactionRequest()
	{
	    PlayerUniqueId = "player456",
	    TransactionId = "1234567890",
		ReversedTransactionId = "234567891"
	};
var reverse_transaction_response = Gameball.ReverseTransaction(request);
```

#### Reverse Hold
```csharp
//Example 1
using Gameball;
using Gameball.Models;
var Gameball = new Gameball(apiKey,transactionKey);

ReverseHoldRequest request = new ReverseHoldRequest()
	{
		PlayerUniqueId = "player456",
		HoldReference = "9245fe4a-d402-451c-b9ed-9c1a04247482"
	};
var reverse_hold_points_response = Gameball.ReverseHold(request);
```

#### Sending Action
```csharp
//Example 1
using Gameball;
using Gameball.Models;
var Gameball = new Gameball(apiKey,transactionKey);

//Creating Request
var request = new ActionRequest()
	{
		PlayerUniqueId = "player123",
		PointsTransaction = new PointsTransaction()
		{
			RewardAmount = 99.98,
			TransactionId = "1234567890"
		}
	};
//Place Order Event			
Event PlaceOrder = new Event()
{
	Name = "place_order"
};
PlaceOrder.AddMetadata("total_amount", "100");
PlaceOrder.AddMetadata("category", new string[] { "electronics", "cosmetics" });
//Review Event
Event Review = new Event()
{
	Name = "review"
};
//Sending Request
request.AddEvent(PlaceOrder);
request.AddEvent(Review);
var send_action_response = Gameball.SendAction(request);

//Example 2
using Gameball;
using Gameball.Models;
var Gameball = new Gameball(apiKey,transactionKey);

//Creating Request
var request = new ActionRequest()
	{
		PlayerUniqueId = "player123",
		PointsTransaction = new PointsTransaction()
		{
			RewardAmount = 99.98,
			TransactionId = "1234567890",
			HoldReference = "2342452352435234"
		},
		PlayerAttributes = new PlayerAttributes()
		{
		 DisplayName = "Tyrion Lannister",
		 FirstName = "Tyrion",
		 LastName = "Lannister",
		 Email = "tyrion@example.com",
		 Gender = "M",
		 DateOfBirth = new DateTime(1978,1,11),
	     JoinDate = new DateTime (2019,9,19,21,6,29,158)
		}
	};
//Adding Custom Attributes
request.PlayerAttributes.AddCustomAttribute("location", "Miami");
request.PlayerAttributes.AddCustomAttribute("graduationDate", new DateTime(2018, 7, 4, 21, 6, 29, 158));
request.PlayerAttributes.AddCustomAttribute("isMarried", false);

//Place Order Event			
Event PlaceOrder = new Event()
{
	Name = "place_order"
};
PlaceOrder.AddMetadata("total_amount", "100");
PlaceOrder.AddMetadata("category", new string[] { "electronics", "cosmetics" });
//Review Event
Event Review = new Event()
{
	Name = "review"
};
//Sending Request
request.AddEvent(PlaceOrder);
request.AddEvent(Review);
var send_action_response = Gameball.SendAction(request);
```


### Handling exceptions
​
Unsuccessful requests raise exceptions. The raised exception will reflect the sort of error that occurred with appropriate message and error code . Please refer to the  [Gameball API docs](https://docs.gameball.co).
​
## Contribution
The master branch of this repository contains the latest stable release of the SDK.
​
## Contact
For usage questions\suggestions drop us an email at support[ at ]gameball.co. Please report any bugs as issues.