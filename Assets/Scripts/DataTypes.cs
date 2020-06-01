namespace h8s
{
    public enum NodeKind { CreateVPC = 0, CreateSubnet = 1 }

    public enum NodeAutomoton { Terraform, Ansible, _Core }

    public enum DataType { Exec, Bool, String, Object }

    public enum PortDirection { Ingress, Egress }
}
