import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import { describe, expect, it, vi } from 'vitest';

import { DomainAvailabilityChecker } from '../src/DomainAvailabilityCheckerPage';

describe('DomainAvailabilityChecker page', () => {
  it('should display base domain check when domain specified by user', async () => {
    //Arrange
    render(<DomainAvailabilityChecker />);

    var mockedResponse = {
      domain: 'tddrocks.com',
      isPremium: true,
      isAvailable: true,
    };
    mockDomainValidatorResponse(mockedResponse);

    //Act
    enterDomainNameAndClickCheckButton('tddrocks.com');

    //Assert
    await waitFor(() => {
      expect(screen.getByText('The searched domain is: tddrocks.com')).toBeInTheDocument();
    });

    await waitFor(() => {
      expect(screen.getByText('Premium domain')).toBeInTheDocument();
    });

    expect(global.fetch).toBeCalledWith(expect.stringContaining('/check?domain=tddrocks.com'));
  });

  it('should display another domain name when specified by user', async () => {
    //Arrange
    render(<DomainAvailabilityChecker />);

    var mockedResponse = {
      domain: 'lifetastesgreat.com',
      isPremium: false,
      isAvailable: true,
    };
    mockDomainValidatorResponse(mockedResponse);

    //Act
    enterDomainNameAndClickCheckButton('lifetastesgreat.com');

    //Assert
    await waitFor(() => {
      expect(screen.getByText('The searched domain is: lifetastesgreat.com')).toBeInTheDocument();
    });

    await waitFor(() => {
      expect(screen.queryByText('Premium domain')).toBeNull();
    });

    expect(global.fetch).toBeCalledWith(
      expect.stringContaining('/check?domain=lifetastesgreat.com'),
    );
  });

  it('should display domain not available when availability flag is false', async () => {
    //Arrange
    render(<DomainAvailabilityChecker />);

    var mockedResponse = {
      domain: 'lifetastesgreat.com',
      isPremium: false,
      isAvailable: false,
    };
    mockDomainValidatorResponse(mockedResponse);

    //Act
    enterDomainNameAndClickCheckButton('lifetastesgreat.com');

    //Assert
    await waitFor(() => {
      expect(screen.getByText('This domain name is not available')).toBeInTheDocument();
    });
  });

  const discountTestCases = [
    { discountCode: 'DISCOUNT_20', expectedDisplayText: '20% OFF' },
    { discountCode: 'DISCOUNT_50', expectedDisplayText: '50% OFF' },
    { discountCode: 'RENEWAL_DISCOUNT_90', expectedDisplayText: '90% RENEWAL DISCOUNT' },
  ];

  it.each(discountTestCases)(
    'should display $expectedDisplayText when discount code is $discountCode',
    async ({ discountCode, expectedDisplayText }) => {
      //Arrange
      render(<DomainAvailabilityChecker />);

      var mockedResponse = {
        domain: 'tddrocks.com',
        isPremium: true,
        isAvailable: true,
        discountCode: discountCode,
      };
      mockDomainValidatorResponse(mockedResponse);

      //Act
      enterDomainNameAndClickCheckButton('tddrocks.com');

      //Assert
      await waitFor(() => {
        expect(screen.getByText(expectedDisplayText)).toBeInTheDocument();
      });
    },
  );

  it('should display No results available in case of API error', async () => {
    //Arrange
    render(<DomainAvailabilityChecker />);

    globalThis.fetch = vi.fn().mockResolvedValue({
      ok: false,
      json: () => Promise.resolve(Error('API error')),
    });

    //Act
    enterDomainNameAndClickCheckButton('tddrocks.com');

    //Assert
    await waitFor(() => {
      expect(screen.getByText('No results available')).toBeInTheDocument();
    });
  });
});

function mockDomainValidatorResponse(mockedResponse: { domain: string; isPremium: boolean }) {
  globalThis.fetch = vi.fn().mockResolvedValue({
    ok: true,
    json: () => Promise.resolve(mockedResponse),
  });
}

function enterDomainNameAndClickCheckButton(domain: string) {
  let inputField = screen.getByRole('textbox');
  fireEvent.change(inputField, { target: { value: domain } });

  let button = screen.getByRole('button');
  fireEvent.click(button);
}
