# Core.Packages

**Core.Packages**, farklı projelerde tekrar tekrar kullanılabilecek ortak kodlamaları ve yardımcı araçları barındıran bir kütüphane projesidir. Bu proje, kod tekrarını azaltmak, tutarlılığı sağlamak ve geliştirme sürecini hızlandırmak amacıyla oluşturulmuştur. Ana projelerimde sıkça ihtiyaç duyduğum temel işlevsellikleri burada topladım ve gerektiğinde diğer projelerime entegre edebiliyorum.

## Proje Amacı

Bu kütüphane, aşağıdaki hedefleri gerçekleştirmek için tasarlandı:
- **Yeniden Kullanılabilirlik**: Ortak kod parçalarını tek bir yerde toplayarak her projede sıfırdan yazma ihtiyacını ortadan kaldırmak.
- **Bakım Kolaylığı**: Merkezi bir yerde tutulan kodların güncellenmesini ve hata düzeltmelerini kolaylaştırmak.
- **Standartlaştırma**: Tüm projelerde tutarlı bir kod yapısı ve yaklaşım sağlamak.

## İçerik

Şu anda Core.Packages aşağıdaki türden kodlamaları içeriyor (içerik projeye göre genişletilebilir):
- **Security**: Kimlik doğrulama, yetkilendirme ve veri güvenliği için yardımcı sınıflar (örneğin, token yönetimi, şifreleme).
- **Repositories**: Genel bir repository pattern uygulaması, veritabanı işlemlerini soyutlamak ve standartlaştırmak için.
- **Sayfalama (Pagination)**: Liste verilerini sayfalama ile yönetmek için yardımcı metodlar ve sınıflar.
- **Loglama (Logging)**: Uygulama genelinde tutarlı loglama mekanizmaları (örneğin, dosya, konsol veya harici servis entegrasyonu).
- **Hata Yönetimi (Error Handling)**: Standart hata yakalama, özel exception sınıfları ve kullanıcı dostu hata mesajları.
- **Pipelines**: Middleware benzeri bir yapı ile authorization, caching, logging, validation ve transaction yönetimini kolaylaştıran araçlar.
- **Yardımcı Sınıflar (Utilities)**: Genel amaçlı yardımcı metodlar (örneğin, string manipülasyonu, tarih işlemleri).
- **Extension Metodlar**: Mevcut .NET türlerine ek işlevsellik katan uzantılar.

## Proje Yapısı

Proje, modüler ve okunabilir bir şekilde düzenlendi:

```bash
Core.Packages/
|── src/
|   |── Core.Application/       
|   |── Core.Infrastructure/    
|
|── Core.Packages.sln                    # Çözüm dosyası
```

## Kullanım

Core.Packages'ı başka bir projede kullanmak için şu adımları izleyebilirsiniz:

1. **NuGet Paketi Olarak Ekleme** (Planlanan):
   - Projeyi bir NuGet paketi olarak paketleyip yerel bir NuGet kaynağından ekleyebilirsiniz (ileride uygulanacak).
   <br><br>
   ```bash
   dotnet add package Core.Packages --version 1.0.0
   ```
   
2. **Proje Referansı Olarak Ekleme**:
   - Çözümünüze Core.Packages.csproj dosyasını ekleyin:
   <br><br>
   ```bash
   dotnet add reference ../Core.Packages/Core.Packages.csproj
   ```

3. **Kodda Kullanım**:
   - İlgili namespace'leri projenize dahil edin ve araçları kullanmaya başlayın:
   <br><br>
   ```bash
   using Core.Packages.Utilities;
   using Core.Packages.Extensions;

   var formattedDate = DateTime.Now.ToFriendlyString(); // Örnek extension metod
   ```
   
## Kurulum

1. **Depoyu Klonlayın**:
   <br><br>
   ```bash
   git clone https://github.com/kullaniciadiniz/Core.Packages.git
   cd Core.Packages

3. **Bağımlılıkları Yükleyin**:
   <br><br>
   ```bash
   dotnet restore Core.Packages.csproj

## Lisans

Bu proje [MIT Lisansı](https://opensource.org/licenses/MIT) kapsamında lisanslanmıştır.
