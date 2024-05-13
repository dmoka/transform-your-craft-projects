import { DomainCheckResponse } from './DomainAvailabilityCheckerPage';

export const processDomainCheckResponse = (response: DomainCheckResponse): string[] => {
  if (!response.isAvailable) {
    return ['This domain name is not available'];
  }

  let checkResults = [];
  checkResults.push(`The searched domain is: ${response.domain}`);
  if (response.isPremium) {
    checkResults.push('Premium domain');
  }

  switch (response.discountCode) {
    case 'DISCOUNT_20':
      checkResults.push('20% OFF');
      break;
    case 'DISCOUNT_50':
      checkResults.push('50% OFF');
      break;
    case 'RENEWAL_DISCOUNT_90':
      checkResults.push('90% RENEWAL DISCOUNT');
      break;
  }

  return checkResults;
};
