# congestion-tax-calculator-volvo
# Improvements Made

## Entry Point and Architecture

**REST Web API:** Introduced a RESTful Web API with layered design.

## Data Management
In the current solution, by using a relational database, the following improvements were achieved:

- **Configurability and Scalability:** Moved tax parameters to the database, enabling the application to serve data for any year. Also made Tax Rates, Toll Free Vehicles, and Dates configurable by moving to the database.
- **Support for Multiple Cities:** Extended support beyond Gothenburg to handle tax calculations for multiple cities.

## Functional Enhancements
- **Single Charge Rule:** Implemented the single charge rule.
- **Exception Handling:** Implemented exception handling to improve error management.
- **Unit Testing:** Utilized xUnit for unit testing to ensure the correctness of the implemented logic.
- **ORM Utilization:** Utilized Dapper ORM for efficient data access and management.

## Documentation
- **API Documentation:** Added Swagger for API documentation and testing.

# Future Improvements

## Increased Test Coverage
Expand test coverage to include additional test cases, covering all code branches.

## Repository Testing
Implement test cases for repository methods to ensure data integrity.

## Caching Mechanism
Implement caching mechanisms for faster response times by caching data from the database.

## Edge Case Handling
Enhance code logic to handle edge cases such as duplicate dates in the tax calculator.

## Integration Testing
Add integration tests to ensure application components are well integrated.