# Revenue Recognition System
API for a software distributor, which allows to keep track of clients, make orders and correctly recognise and calculate revenue.

## User types:
1. Basic user
2. Administrator

## Functionalities

### Managing clients
**Domain models:**
1. Client
  a. Individual
  b. Company

**Use cases:**
 1. Add client,
 2. Remove client (only available to admin)
 3. Update data about client (only available to admin)

### Managing contracts
**Domain models:**
1. Product (Software available for distribution)
2. Version (Version of a specific product)
3. Discount (Discount for a specific product or a group of products)
4. Contract (Contract for a specific product, signed by client, which has to be paid)
5. Transaction (Payment issued for an active contract)

**Use cases:**
1. Create contract
2. Register payment

### Calculating revenue
**Use cases:**
1. Calculate revenue (for all products/specific product, with an option to convert result to a different currency)
   a. Calculate real revenue
   b. Calculate predicted revenue (i.e. including unsigned contracts)
