﻿using TheNextBigThing.Application.Clients;
using TheNextBigThing.Application.Utilities;
using TheNextBigThing.Domain.DTO;
using TheNextBigThing.Domain.Entities;
using TheNextBigThing.Domain.Exceptions;
using TheNextBigThing.Domain.Interfaces;
using TheNextBigThing.Domain.Responses;

namespace TheNextBigThing.Application.Services;

public class CurrencyRateService
{
    private readonly LBCurrencyClient _client;
    private readonly ICurrencyRateRepository _rateRepository;

    public CurrencyRateService(LBCurrencyClient client, ICurrencyRateRepository currencyRateRepository)
    {
        _client = client;
        _rateRepository = currencyRateRepository;
    }

    public async Task<RateChangesResponse> GetRateChanges(DateTime date)
    {
        CurrencyDataEntity? current = null;// await _rateRepository.Get(date);
        if (current is null)
        {
            current = await DownloadRates(date);
            //await _rateRepository.Store(current);
        }

        CurrencyDataEntity? previous = null; // await _rateRepository.Get(date);
        if (previous is null)
        {
            previous = await DownloadRates(date.AddDays(-1));
            //await _rateRepository.Store(previous);
        }

        ExchangeRates? curRates = XmlUtility.Deserialize<ExchangeRates>(current.RatesXml);
        ExchangeRates? prevRates = XmlUtility.Deserialize<ExchangeRates>(previous.RatesXml);

        if (curRates is null || prevRates is null)
        {
            throw new CurrencyDataException("Error deserializing currency data.");
        }

        return new RateChangesResponse()
        {
            Date = date,
            Changes = CalculateChanges(curRates, prevRates)
        };
    }

    private async Task<CurrencyDataEntity> DownloadRates(DateTime date)
    {
        return new CurrencyDataEntity()
        {
            Date = date,
            RatesXml = await _client.Get(date)
        };
    }

    private List<RateChange> CalculateChanges(ExchangeRates current, ExchangeRates previous)
    {
        if (current.Items.Count != previous.Items.Count)
        {
            throw new CurrencyDataException("Cannot calculate changes: current and previous currency counts are different.");
        }

        List<RateChange> changes = new List<RateChange>();

        for (int i = 0; i < current.Items.Count; ++i)
        {
            if (current.Items[i].Currency == previous.Items[i].Currency)
            {
                changes.Add(new RateChange()
                {
                    Name = current.Items[i].Currency,
                    Change = current.Items[i].Rate / current.Items[i].Quantity - previous.Items[i].Rate / previous.Items[i].Quantity
                });
            }
        }

        return changes;
    }
}
