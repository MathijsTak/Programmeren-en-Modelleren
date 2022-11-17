from datetime import datetime, timedelta

vandaag = datetime.now()
verKdag = None
leeftijd = 0

# Vraag de user om zijn/haar verjaardag en zet dit om naar aparte integers
verjaardag = input("Wanneer ben je jarig? (dd-mm-YYYY)\n")

verjaardag = datetime.strptime(verjaardag, "%d-%m-%Y")

# Totdat er een verKdag later dan vandaag of vandaag is gevonden voeg 1000 toe aan de verjaardag
while verKdag == None:
    leeftijd += 1
    datum = verjaardag + timedelta(days=leeftijd * 1000)
    if datum > vandaag:
        verschil = datum - vandaag
        verschil = verschil.days
        datum = datum.strftime("%d-%m-%Y")
        verKdag = f"over {verschil} dagen, op {datum}"
    if datum == vandaag:
        verKdag = "vandaag"



# Output is de verKdagen en de volgende verKdag
print(f"Je hebt al {leeftijd} verKdag(en) gehad en je volgende verKdag is {verKdag}.")