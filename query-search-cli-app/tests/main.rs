use assert_cmd::Command;

#[test]
fn should_print_single_line_when_matched_with_single() {
    let mut cmd = Command::cargo_bin("cli").unwrap();

    cmd.arg("joyful life")
        .arg("./tests/happiness.txt")
        .assert()
        .stdout(predicates::str::contains(
            "4: Savor the simple joys for a joyful life.",
        ));
}

#[test]
fn should_print_multiple_lines_when_matched_with_multiple() {
    let mut cmd = Command::cargo_bin("cli").unwrap();

    cmd.arg("Happiness")
        .arg("./tests/happiness.txt")
        .assert()
        .stdout(predicates::str::contains(
            "2: Happiness is found in the little things.\n5: In simplicity, we discover true happiness.",
        ));
}

#[test]
fn should_fail_when_no_file_path_specified() {
    let mut cmd = Command::cargo_bin("cli").unwrap();

    cmd.arg("joyful life")
        .assert()
        .failure()
        .stderr(predicates::str::contains(
            "Parsing arguments error: not enough arguments specified",
        ));
}

#[test]
fn should_fail_when_no_args_specified_at_all() {
    let mut cmd = Command::cargo_bin("cli").unwrap();

    cmd.assert().failure().stderr(predicates::str::contains(
        "Parsing arguments error: not enough arguments specified",
    ));
}

#[test]
fn should_fail_when_file_doesnt_exist() {
    let mut cmd = Command::cargo_bin("cli").unwrap();

    cmd.arg("joyful life")
        .arg("./tests/non_existing.txt")
        .assert()
        .failure()
        .stderr(predicates::str::contains(
            "Application error: The system cannot find the file specified",
        ));
}
