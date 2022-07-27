# LINQ .NET Core Code Samples

This project demonstrates the use of LINQ, JSON, C#.

## Background

To add a bit of flair to the code sample, an app for a hypothetical coffee shop is selected as the backdrop. 

The app keeps track of coffee ordered; what the balance is for each user; what users have paid for already; and what is still owed.

## Data

The following datasets are used:

- `data/prices.json` - provided by a barista. Has details of what beverages are available, and what their prices are.
- `data/orders.json` - list of beverages ordered by users of the app.
- `data/payments.json` - list of payments made by users paying for items they have purchased.

## Requirements

### Requirement 1

Print all orders to the console.

### Requirement 2

Please count by user how many orders they have had.  Expected output:
```JSON
    [
        {
            "name": "Ellis",
            "count": 5
        }
    ]
```

### Requirement 3

Use the data from `orders.json` and `prices.json` to find the total cost for each user.  Expected output:

```JSON
    [
        {
            "name": "Ellis",
            "count": 5,
            "balance": 80.50
        }
    ]
```

### Requirement 4

Write any unit tests required to validate the intent of the code from Requirement 3

### Requirement 5

Reconcile the `payments.json` file with the output from Requirement 3.  Expected output:
```JSON
    [
        {
            "user": "coach",
            "orderTotal": 8.00,
            "paymentTotal": 2.50,
            "balance": 5.50
        }
    ]
```

### Requirement 6

`orders.json` has an unsupported order type.
