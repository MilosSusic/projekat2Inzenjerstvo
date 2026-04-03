# Projekat 2 - Softversko Inženjerstvo (Projekat2Inzenjerstvo)

Ovo je repozitorijum za drugi projektni zadatak (PZ2) razvijen prema priloženoj specifikaciji. Projekat je u potpunosti implementiran u jeziku **C#** i predstavlja sistem za simulaciju, nadzor i upravljanje entitetima u mrežnom sistemu. 

Razvijen je kao klijent-server (ili simulator-servis) sistem koristeći **WPF (Windows Presentation Foundation)** uz poštovanje **MVVM (Model-View-ViewModel)** arhitektonskog šablona.

---

## ⚙️ Ključne funkcionalnosti (NetworkService)

Glavna aplikacija (`NetworkService`) je podeljena u nekoliko logičkih celina (pogleda) kroz koje korisnik može vršiti interakciju sa sistemom:

### 1. Upravljanje entitetima (Network Entities)
Korisnički interfejs (`NetworkEntities.xaml`) pruža funkcionalnosti za rad sa mrežnim entitetima (modeli `Production` i `Type`). 
* **Dodavanje novih entiteta** u sistem sa specifičnim parametrima i tipom.
* **Ažuriranje postojećih parametara** i brisanje entiteta.
* **Filtriranje i pretraga** kreiranih elemenata radi lakšeg snalaženja u tabelarnom prikazu.

### 2. Vizuelni prikaz mreže (Network Display)
Deo aplikacije (`NetworkDisplay.xaml`) omogućava vizuelnu reprezentaciju fizičke mreže.
* **Interaktivni Grid/Platno:** Mogućnost prevlačenja (Drag & Drop) i raspoređivanja entiteta na radnoj površini.
* **Povezivanje:** Rad sa linijskim povezivanjem (model `Line.cs`) entiteta kako bi se definisala struktura i topologija.
* **Prikaz stanja:** Dinamička promena vizuelnog prikaza na osnovu podataka koji stižu sa simulatora (npr. upozorenja kada vrednosti iskoče iz predviđenog opsega).

### 3. Prikaz merenja na grafiku (Measurement Graph)
Vizuelizacija praćenja očitanih vrednosti tokom vremena (`MeasurementGraph.xaml`).
* Crtanje **dinamičkih grafika** korišćenjem podataka očitavanja (model `GraphValues.cs`).
* Prikaz istorije i fluktuacija vrednosti koje pristižu od simulatora za odabrani mrežni entitet.
* Uočavanje kritičnih ili vanrednih vrednosti i vizuelna obeležavanja na samom grafiku.

---

## 📡 Simulator merenja (MeteringSimulator)

Za potpuno funkcionisanje sistema koristi se i odvojeni projekat **MeteringSimulator**.
* **Generisanje vrednosti:** Ovaj servis konstantno, u realnom vremenu (ili po određenom vremenskom intervalu), simulira promenu stanja/parametara na uređajima u mreži.
* **Komunikacija:** Prosleđuje generisane podatke ka glavnom servisu (`NetworkService`), na osnovu kojih glavna aplikacija automatski osvežava vrednosti grafika i alarmira ukoliko su parametri van granica normale.

---

## 🚀 Instalacija i pokretanje

1. Klonirajte repozitorijum:
   ```bash
   git clone https://github.com/MilosSusic/projekat2Inzenjerstvo.git
   ```

2. Otvorite rešenje u **Visual Studio** okruženju:
   Pokrenite fajl `NetworkService.sln` koji se nalazi u folderu `CG2T4G1P2/NetworkService/`.

3. **Podešavanje okruženja (Multiple Startup Projects)**:
   Pošto sistem zahteva da simulator i servis rade u isto vreme:
   * Desni klik na Solution (na vrhu *Solution Explorer-a*) $\rightarrow$ **Properties**
   * Prebacite na **Multiple startup projects**
   * Postavite `Action` kolonu na **Start** i za *NetworkService* i za *MeteringSimulator*.

4. Pokrenite aplikaciju pritiskom na dugme **Start** (ili `F5`).

---

## 🛠️ Korišćene tehnologije
- **C# / .NET**
- **WPF** – Za izgradnju bogatog interfejsa.
- **MVVM patern** – Model klasama odvojena logika, BindableBase implementiran za automatsko ažuriranje View-a.

## 👤 Autor

- [MilosSusic](https://github.com/MilosSusic)
