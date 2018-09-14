<img src="assets/rounder-upper.png" width="200">

# Starling Bank Rounder Upper

Every penny helps. That's why as part of our personal development day at [Answer Digital](https://answerdigital.com), I opted to take advantage of Starling's APIs to round-up outgoing transactions.

At the beginning of September 2018, I started to round up the transactions manually and it soon got tedious! The solution, automate it.

## Prerequisites

- Starling Bank Account
- Starling Developer Account
- Azure Account

## Technologies

// TODO

## Settings

| Key | Type | Example |
|-----|------|---------|
|`ROUND_UP_THRESHOLD` | int | 5 |
|`STARLING_GOAL_ID` | string | dc040d7d-4ddf-48ea-85a6-853c514421a3 |
|`STARLING_ACCESS_TOKEN` | string | llLXtLousnRadmfmN9LEqzq8LLPnW211DyfCrj1ANmxvaCacC0vN2doMA4D3raEY |
|`STARLING_WEBHOOK_SECRET` | string | 5fbb03c9-e22b-486e-bc92-bf37cbfe3647 |
|`STARLING_BASE_URL` | string | https://api.starlingbank.com/api |

> `ROUND_UP_THRESHOLD` is currently not in use. 

## Personal Access Tokens & Webhook Types

For this to work, the following scopes are required:

 - `transaction:read`
 - `savings-goal:read`
 - `savings-goal-transfer:read`
 - `savings-goal-transfer:create`

Webhook types required:

- Faster Payment Out
- Direct Debit
- Mobile Wallet
- Card