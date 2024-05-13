/// Scenario: Check domain availability
///
/// Given I open the domain checker page
/// When I provide a domain
/// And I click on check button
/// Then I see the availability of the domain
///
describe('Check Domain Availability', () => {
  it('Should check and display domain availability results', () => {
    GivenUserOpensPage();

    WhenEntersDomain('www.tddrocks.com');
    WhenClicksOnCheckButton();

    ThenAssertThatCheckResultHasItemWithText(0, 'The searched domain is: www.tddrocks.com');
    ThenAssertThatCheckResultHasItemWithText(1, 'Premium domain');
    ThenAssertThatCheckResultHasItemWithText(2, '50% OFF');
  });
});

function ThenAssertThatCheckResultHasItemWithText(index: number, expectedText: string) {
  cy.get('#analyses-result li').eq(index).should('have.text', expectedText);
}

function GivenUserOpensPage() {
  cy.visit('/');
  cy.intercept('GET', 'http://localhost:9000/check?domain=www.tddrocks.com', {
    statusCode: 200,
    body: {
      domain: 'www.tddrocks.com',
      isAvailable: true,
      isPremium: true,
      discountCode: 'DISCOUNT_50',
    },
  }).as('domainCheck');
}

function WhenEntersDomain(domain: string) {
  cy.get('input[data-test="domain-field"]').type(domain);
}

function WhenClicksOnCheckButton() {
  cy.get('.check-button').click();
}
