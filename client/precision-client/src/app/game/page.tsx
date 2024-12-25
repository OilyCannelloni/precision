import {Deal, DealModel} from "@/components/deal"

export default function Page() {
    return <span>
               {Deal(DealModel.from_str("T983.743.A9.K964 Q752.AT82.QJT7.5 4.KQJ9.K8652.Q87 AKJ6.65.43.AJT32"))} 
    </span>;
}