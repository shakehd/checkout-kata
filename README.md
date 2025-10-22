# Checkout Kata

This project implements a solution for the classic Supermarket Checkout programming kata, as described in [Kata09: Back to the Checkout](http://codekata.com/kata/kata09-back-to-the-checkout/).

## Problem Description

The goal is to build a checkout system that can scan items and calculate the total price. The complexity comes from the pricing rules. Some items have a simple unit price, while others have special offers. For example:

| Item | Unit Price | Special Offer |
| :--- | :--- | :--- |
| A    | 50         | 3 for 130     |
| B    | 30         | 2 for 45      |
| C    | 20         |               |
| D    | 15         |               |

The system needs to be flexible enough to handle these different pricing schemes. The kata encourages an incremental approach, starting with simple unit pricing and then adding the complexity of special offers. This project implements a solution that is both correct and easily extensible for new pricing rules.

## Project Structure

The solution is organized into a clean and modular structure, promoting separation of concerns and ease of maintenance.

```
/
─── checkout/
    ├── Pricing/
    │   ├── IPricingStrategy.cs
    │   ├── IPricingStrategyProvider.cs
    │   └── PricingStrategy.cs
    ├── Validation/
    │   ├── NonEmptyAndNullString.cs
    │   ├── NonNegativeNumber.cs
    │   └── PositiveNumber.cs
    ├── ICheckout.cs
    ├── Checkout.cs
    └── Result.cs
```

### Core Components

*   **`ICheckout` & `Checkout.cs`**: These define the main contract and implementation of the checkout system. The `Checkout` class is responsible for scanning items and calculating the total price.

*   **`Result.cs`**: A simple discriminated union to represent the outcome of an operation (either success or an error), preventing the use of exceptions for control flow.

### Pricing Subsystem (`/Pricing`)

This directory contains all the logic related to calculating prices.

*   **`IPricingStrategy`**: An interface that defines the contract for any pricing strategy. This allows for different pricing models (e.g., per-unit, special offers) to be used interchangeably.
*   **`PricingStrategy.cs`**: Contains concrete implementations of `IPricingStrategy`, such as `UnitPricing` and `SpecialPricing` for bundle offers.
*   **`IPricingStrategyProvider`**: An interface for a service that provides the correct pricing strategy for a given item SKU. This decouples the `Checkout` class from the specific pricing rules.

### Validation Subsystem (`/Validation`)

This directory contains small, reusable record types that enforce data validation at the type level, making the domain logic more robust.

*   **`NonNegativeNumber.cs`**: Represents an integer that cannot be negative.
*   **`PositiveNumber.cs`**: Represents an integer that must be greater than zero.
*   **`NonEmptyAndNullString.cs`**: Represents a string that cannot be null or empty.

### Testing (`/checkout.tests`)

*   **`CheckoutTests.cs`**: Contains unit tests to verify the correctness of the checkout system's logic and calculations.

## How to Use

1.  Instantiate a `Checkout` class with a concrete implementation of `IPricingStrategyProvider`.
2.  Call the `Scan()` method for each item.
3.  Call the `GetTotalPrice()` method to get the final calculated price.
