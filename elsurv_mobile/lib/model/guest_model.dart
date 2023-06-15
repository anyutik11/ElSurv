class Guest {
  final String id;
  final String gkey;
  final String surname;
  final String name;
  final String email;
  final String phone;
  final String dateBirth;
  final String gender;

  const Guest({
    required this.id,
    required this.gkey,
    required this.surname,
    required this.name,
    required this.email,
    required this.phone,
    required this.dateBirth,
    required this.gender,
  });

  factory Guest.fromJson(Map<String, dynamic> json) {
    return Guest(
      id: json['id'],
      gkey: json['gkey'],
      surname: json['surname'],
      name: json['name'],
      email: json['email'],
      phone: json['phone'],
      dateBirth: json['dateBirth'],
      gender: json['gender'],
    );
  }
}
