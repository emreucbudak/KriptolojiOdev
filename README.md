#🛡️ Kriptoloji – Form Üzerinden Şifreleme pROGRAMI


**Bu proje, TCP üzerinden çalışan bir client-server uygulamasıdır ve Windows Forms üzerinden şifreleme işlemleri yapılmasını sağlar.

**🌐 Client: Kullanıcının girdiği metni ve şifreleme key’ini alır, server’a gönderir.

**🖥️ Server: Mesajı alır, seçilen algoritmaya göre şifreler ve client’a geri yollar.

##✨ Desteklenen Şifreleme Algoritmaları

**🔑 Vigenere Cipher – Key kullanıcıdan alınır, sadece harflerden oluşmalı (A-Z).

**🔑 Substitution Cipher – 26 farklı harf, her harf benzersiz olmalı.

**🔑 Caesar Cipher – Sabit kaydırma: +3.

**🔑 Affine Cipher – Basit affine algoritması ile şifreleme.
##⚡ Kullanım
**1) Şifrelenmesi istenen metin girilir
**2) Eğer İlgili Şifreleme Türü için key gerekiyorsa key girilir
**3) İstenilen Şifreleme türünün butonuna basılır
**4) Program arka planda Tcp-Server üzerinden serverla bağlantıyı kurar şifrelemeyi gerçekleştirir ve kullanıcıya geri döner
##📸 Uygulamadan Ekran Görüntüleri
**1) Sunucu Başlatılmadan Önce:
<img width="1153" height="528" alt="image" src="https://github.com/user-attachments/assets/ddf718f3-1bb1-4d91-8bab-561f4f4a2a12" />
**2) Sunucu Başlatıldığında:
<img width="1153" height="485" alt="image" src="https://github.com/user-attachments/assets/e7faa831-8dbd-499e-b360-954a2d70fe01" />
**3) Client tarafında key gerektirmeyen şifrelemelerde 
<img width="1063" height="545" alt="image" src="https://github.com/user-attachments/assets/e1027c5b-5e45-4a9e-994f-629234fd2b2f" />
**4)Client tarafında key gerektiren şifrelemelerde 
<img width="1067" height="483" alt="image" src="https://github.com/user-attachments/assets/19322d96-920f-4203-8727-00b209f9aff3" />



