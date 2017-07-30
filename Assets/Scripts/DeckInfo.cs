﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckInfo : MonoBehaviour
{

    //The cards that are in the player's deck
    Stack<Card> deck = new Stack<Card>();

    // Use this for initialization
    void Start()
    {

        if (transform.name.Contains("1"))
        {
            var localDeck = Decks.player1Deck;

            while (localDeck.Count > 0)
            {
                var localCard = localDeck[Random.Range(0, localDeck.Count)];
                deck.Push(localCard);
                localDeck.Remove(localCard);
            }
        }
        else if (transform.name.Contains("2"))
        {
            var localDeck = Decks.player2Deck;

            while (localDeck.Count > 0)
            {
                var localCard = localDeck[Random.Range(0, localDeck.Count)];
                deck.Push(localCard);
                localDeck.Remove(localCard);
            }
        }

        if (deck.Count == 0)
        {
            //TEMPERARYILY create a deck of random cards
            for (int i = 0; i < 40; i++)
            {
                deck.Push(CardList.cards[Random.Range(0, CardList.cards.Count)]);
            }
        }

        //Please three cards into the player's hand from their deck
        for (int i = 0; i < 3; i++)
        {
            transform.FindChild("Cards").GetComponent<UsingCard>().DrawnCard(deck.Pop());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DrawCard()
    {
        if (deck.Count <= 0)
        {
            GetComponent<PlayerPower>().powerPerTurn--;
            return;
        }

        //Try and place a card into the players hand
        //Remove the card from the deck if the card was put into the player's hand
        if (transform.FindChild("Cards").GetComponent<UsingCard>().DrawnCard(deck.Peek()))
        {
            deck.Pop();

            //If the card didn't go into the player's hand penalize them
            //They lose 1 energy production per turn
        }
        else
        {
            GetComponent<PlayerPower>().powerPerTurn--;
        }
    }
}
