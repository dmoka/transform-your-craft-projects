# Trendy Domain Name Checker

## Overview

Enable users to check availability of domains together with their status and discounts

## Acceptance criteria

### 1. Create a page with form

- Domain input field
- Check button

### 2. Send domain to 3rd party domain validator

### 3. Display domain checks

- Display `The searched domain is: <domainName>` in case of available domain
- Display `Premium domain` if premium flag is true
- Display `This domain name is not available` when availability flag is false
- Display `20% OFF` in case of discount code `DISCOUNT_20`
- Display `50% OFF` in case of discount code `DISCOUNT_50`
- Display `90% RENEWAL DISCOUNT` in case of discount code `RENEWAL_DISCOUNT_90`

### 4. Handle error

- Display `No results available` in case of API error

## API Specification of 3rd party domain validator

Request:

```
/check?domain=<domainName>
```

Response fields:

- domain (string)
- isPremium (boolean)
- isAvailable (boolean)
- discountCode (DISCOUNT_20 | DISCOUNT_50 | RENEWAL_DICOUNT_90)
