class Company {
  final String id;
  final bool deleted;
  final String fullName;
  final String shortName;
  final String contactPhone;
  final String contactEmail;

  const Company({
    required this.id,
    required this.deleted,
    required this.fullName,
    required this.shortName,
    required this.contactPhone,
    required this.contactEmail,
  });

  factory Company.fromJson(Map<String, dynamic> json) {
    return Company(
      id: json['id'],
      deleted: json['deleted'],
      fullName: json['fullName'],
      shortName: json['shortName'],
      contactPhone: json['contactPhone'],
      contactEmail: json['contactEmail'],
    );
  }
}
